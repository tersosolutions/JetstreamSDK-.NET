﻿﻿/*
     Copyright 2012 Terso Solutions

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
//using Amazon.SQS;
//using Amazon.SQS.Model;
using Newtonsoft.Json;
using AE = TersoSolutions.Jetstream.Application.SQS.AggregateEvent;
using CCE = TersoSolutions.Jetstream.Application.SQS.CommandCompletionEvent;
using CQE = TersoSolutions.Jetstream.Application.SQS.CommandQueuedEvent;
using DFE = TersoSolutions.Jetstream.Application.SQS.DeviceFailureEvent;
using DRE = TersoSolutions.Jetstream.Application.SQS.DeviceRestoreEvent;
using HE = TersoSolutions.Jetstream.Application.SQS.HeartbeatEvent;
using LDAE = TersoSolutions.Jetstream.Application.SQS.LogicalDeviceAddedEvent;
using LDRE = TersoSolutions.Jetstream.Application.SQS.LogicalDeviceRemovedEvent;
using LEE = TersoSolutions.Jetstream.Application.SQS.LogEntryEvent;
using OE = TersoSolutions.Jetstream.Application.SQS.ObjectEvent;
using SRE = TersoSolutions.Jetstream.Application.SQS.SensorReadingEvent;
using TersoSolutions.Jetstream.Application.Model;

namespace TersoSolutions.Jetstream.Application.Events
{
    /// <summary>
    /// abstract windows service class that pops messages from a SQS queue that is a subscriber of a Jetstream SNS topic
    /// </summary>
    /// <remarks>Author Mike Lohmeie</remarks>
    public abstract class JetstreamEventService : ServiceBase
    {

        #region Data

        private Object _newWindowLock = new object();
        private Timer _windowTimer;
        private Object _windowLock = new object();
        private CancellationTokenSource _cts;
        private Object _setLock = new object();
        private List<object> _set = new List<object>(); //use a  Red-Black tree for the producer-consumer data store
        private string _currentBatchId = string.Empty;
        private volatile bool _isWindowing = false;
        private const String c_DEFAULTAWSSQSSERVICEURL = "sqs.us-east-1.amazonaws.com";
        private event EventHandler<NewWindowEventArgs> NewWindow;

        #endregion

        #region Properties

        /// <summary>
        /// Get to determine if Gzip is enabled for the application.  Defaults to false.  Settable by putting an appSettings boolean with key GzipEnabled.
        /// </summary>
        private bool GzipEnabled
        {
            get
            {
                bool enabled = false;
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["GzipEnabled"]))
                {
                    Boolean.TryParse(ConfigurationManager.AppSettings["GzipEnabled"], out enabled);
                }
                return enabled;
            }
        }

        /// <summary>
        /// The TimeSpan that all results from SQS are windowed and sorted before firing the NewWindow event
        /// </summary>
        protected TimeSpan MessageCheckWindow
        {
            get
            {
                TimeSpan result;
                if ((ConfigurationManager.AppSettings["MessageCheckWindow"] != null) &&
                    (TimeSpan.TryParse(ConfigurationManager.AppSettings["MessageCheckWindow"], out result)))
                {
                    if ((result.TotalDays <= 1) & (result.TotalSeconds >= 1))
                    {
                        return result;
                    }
                    else if (result.TotalDays > 1)
                    {
                        return new TimeSpan(1, 0, 0, 0);
                    }
                    else
                    {
                        return new TimeSpan(0, 0, 1);
                    }
                }
                else
                {
                    // default to a 1 minute window
                    return new TimeSpan(0, 1, 0);
                }
            }
        }

        /// <summary>
        /// The Amazon Simple Queue Service Url
        /// </summary>
        protected String JetstreamUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["JetstreamUrl"];
            }
        }

        /// <summary>
        /// The Amazon access key to access SQS with
        /// </summary>
        protected String UserAccessKey
        {
            get
            {
                return ConfigurationManager.AppSettings["UserAccessKey"];
            }
        }



        /// <summary>
        /// Indicates if the SQSService is currently polling/windowing events
        /// </summary>
        public bool IsWindowing
        {
            get
            {
                return _isWindowing;
            }
            private set
            {
                _isWindowing = value;
            }
        }

        #endregion

        #region Service Events

        /// <summary>
        /// override the OnStart for the windows service to hook the NewWindow event
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            // validate that the service is configured correctly
            if (String.IsNullOrEmpty(this.UserAccessKey)) throw new ConfigurationErrorsException("There is no UserAccesskey in the appSettings");
            if (String.IsNullOrEmpty(this.JetstreamUrl)) throw new ConfigurationErrorsException("There is no JetstreamUrl in the appSettings");

            // start all of the background processing
            StartProcesses();

            // hook an event handler to the NewWindow event
            this.NewWindow += new System.EventHandler<NewWindowEventArgs>(JetstreamService_NewWindow);

            // call the base
            base.OnStart(args);
        }

        /// <summary>
        /// Override of the WindowsService Stop
        /// </summary>
        protected override void OnStop()
        {
            // stop all of the background processing
            StopProcesses();

            base.OnStop();
        }

        /// <summary>
        /// Override of the Pause
        /// </summary>
        protected override void OnPause()
        {
            // stop all of the background processing
            StopProcesses();

            base.OnPause();
        }

        /// <summary>
        /// Override of the Continue
        /// </summary>
        protected override void OnContinue()
        {
            // start all of the background processing
            StartProcesses();

            base.OnContinue();
        }

        /// <summary>
        /// Override of the Power Event
        /// </summary>
        /// <param name="powerStatus"></param>
        /// <returns></returns>
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            if ((powerStatus == PowerBroadcastStatus.BatteryLow) |
                (powerStatus == PowerBroadcastStatus.Suspend))
            {
                // stop the background processing
                StopProcesses();
            }

            if ((powerStatus == PowerBroadcastStatus.ResumeAutomatic) |
                (powerStatus == PowerBroadcastStatus.ResumeCritical) |
                (powerStatus == PowerBroadcastStatus.ResumeSuspend))
            {
                // start the background processing
                StartProcesses();
            }

            return base.OnPowerEvent(powerStatus);
        }

        /// <summary>
        /// Override of the Shutdown
        /// </summary>
        protected override void OnShutdown()
        {
            // stop the background processing
            StopProcesses();

            base.OnShutdown();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Task for Receiving messages from SQS
        /// </summary>
        /// <param name="state"></param>
        private void ReceiveTask(CancellationToken ct, int numberOfMessages)
        {
            try
            {
                JetstreamServiceClient client = new JetstreamServiceClient(this.JetstreamUrl, this.UserAccessKey);
                GetEventsRequest request = new GetEventsRequest();
                GetEventsResponse response = client.GetEvents(request);
                _currentBatchId = response.BatchId;
                List<object> messages = new List<object>();
                foreach (var message in response.Events)
                {
                    messages.Add(message);
                }

                // now add the Messages to the data store for the window thread
                if (!ct.IsCancellationRequested)
                {
                    lock (_setLock)
                    {
                        for (int i = 0; i < messages.Count; i++)
                        {
                            if (ct.IsCancellationRequested)
                            {
                                break;
                            }
                            _set.Add(messages[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("JetstreamSDK",
                    ex.Message + "\n" + ex.StackTrace,
                    EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Task for deleting messages from SQS
        /// </summary>
        /// <param name="state"></param>
        private void DeleteTask(CancellationToken ct, List<object> messages, Tuple<int, int> range)
        {
            try
            {
                JetstreamServiceClient client = new JetstreamServiceClient(this.JetstreamUrl, this.UserAccessKey);
                RemoveEventsRequest request = new RemoveEventsRequest();
                request.BatchId = _currentBatchId;
                //RemoveEventsResponse response = client.RemoveEvents(request);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("JetstreamSDK",
                    ex.Message + "\n" + ex.StackTrace,
                    EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Callback for windowing the SQS messages ordered by time queued
        /// </summary>
        /// <param name="state"></param>
        private void WindowCallback(Object state)
        {
            try
            {
                CancellationToken ct = (CancellationToken)state;
                List<object> messages = new List<object>();

                // syncronize the processing of windows
                if ((!ct.IsCancellationRequested) &
                    (Monitor.TryEnter(_windowLock, 1000)))
                {
                    try
                    {
                        long epochWindowTime = ToEpochTimeInMilliseconds(DateTime.UtcNow.Subtract(this.MessageCheckWindow));
                        int numberOfMessages = 0;

                        // just do it inline it's less expensive than spinning threads
                        ReceiveTask(ct, numberOfMessages);

                        // ok so all messages have been Receieved and ordered or no more messages can be popped
                        if (!ct.IsCancellationRequested)
                        {
                            foreach (var message in _set)
                            {
                                messages.Add(message);
                            }

                            // remove duplicates
                            //TODO Find a better way to dedup that doesn't destroy the order of the array
                            //IEnumerable<object> dedupMessages = messages.Distinct(new MessageEqualityComparer());

                            // distinct doesn't presever order so we need to reorder the window
                            //IEnumerable<object> orderedMessages = dedupMessages.OrderBy(m => m, new MessageSentTimeStampComparer());

                            // raise the WindowEvent with the results
                            if (!ct.IsCancellationRequested)
                            {
                                OnNewWindow(messages);
                            }
                        }

                    }
                    finally
                    {
                        Monitor.Exit(_windowLock);
                    }
                }

                // check if we should delete the messages
                if ((!ct.IsCancellationRequested) &
                    (messages.Count > 0))
                {
                    DeleteTask(ct, messages, new Tuple<int, int>(0, messages.Count));
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("JetstreamSDK",
                    ex.Message + "\n" + ex.StackTrace,
                    EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Starts all of the background processing
        /// </summary>
        private void StartProcesses()
        {
            // create a new cancellationtoken souce
            _cts = new CancellationTokenSource();

            // start the window timer
            _windowTimer = new Timer(new TimerCallback(WindowCallback),
                _cts.Token, 0, Convert.ToInt64(this.MessageCheckWindow.TotalMilliseconds));

            this.IsWindowing = true;
        }

        /// <summary>
        /// Cancels all of the background processing
        /// </summary>
        private void StopProcesses()
        {
            // signal a cancel to the background threads
            _cts.Cancel();

            // give the background threads a bit of time to cancel
            Thread.Sleep(100);

            // now dispose of them
            if (_windowTimer != null)
            {
                _windowTimer.Dispose();
            }

            this.IsWindowing = false;
        }

        /// <summary>
        /// On* event raising pattern implementation for the NewWindow event
        /// </summary>
        /// <param name="messages"></param>
        private void OnNewWindow(IEnumerable<object> messages)
        {
            if (NewWindow != null)
            {
                NewWindow(this, new NewWindowEventArgs(messages));
            }
        }

        /// <summary>
        /// Event handler for the NewWindow event
        /// </summary>
        /// <param name="sender">SQSService</param>
        /// <param name="e">The NewWindow event args</param>
        private void JetstreamService_NewWindow(object sender, NewWindowEventArgs e)
        {
            // lock so we process all events in order
            lock (_newWindowLock)
            {
                foreach (var m in e.Messages)
                {
                    try
                    {
                        string x = m.GetType().ToString();

                        Debug.WriteLine("Jetstream Message: " + x);

                        // now we can deserialize the XML message into the appropriate message
                        switch (x.Trim().ToLower())
                        {
                            case "tersosolutions.jetstream.application.sqs.aggregateevent.jetstream":
                                {
                                    AE.Jetstream message = (AE.Jetstream)m;
                                    ProcessAggregateEvent(message);
                                    break;
                                }
                            case "tersosolutions.jetstream.application.sqs.commandcompletionevent.jetstream":
                                {
                                    CCE.Jetstream message = (CCE.Jetstream)m;
                                    ProcessCommandCompletionEvent(message);
                                    break;
                                }
                            case "tersosolutions.jetstream.application.sqs.commandqueuedevent.jetstream":
                                {
                                    CQE.Jetstream message = (CQE.Jetstream)m;
                                    ProcessCommandQueuedEvent(message);
                                    break;
                                }
                            case "tersosolutions.jetstream.application.sqs.devicefailureevent.jetstream":
                                {
                                    DFE.Jetstream message = (DFE.Jetstream)m;
                                    ProcessDeviceFailureEvent(message);
                                    break;
                                }
                            case "tersosolutions.jetstream.application.sqs.devicerestoreevent.jetstream":
                                {
                                    DRE.Jetstream message = (DRE.Jetstream)m;
                                    ProcessDeviceRestoreEvent(message);
                                    break;
                                }
                            case "tersosolutions.jetstream.application.sqs.heartbeatevent.jetstream":
                                {
                                    HE.Jetstream message = (HE.Jetstream)m;
                                    ProcessHeartbeatEvent(message);
                                    break;
                                }
                            case "tersosolutions.jetstream.application.sqs.logentryevent.jetstream":
                                {
                                    LEE.Jetstream message = (LEE.Jetstream)m;
                                    ProcessLogEntryEvent(message);
                                    break;
                                }
                            case "tersosolutions.jetstream.application.sqs.logicaldeviceaddedevent.jetstream":
                                {
                                    LDAE.Jetstream message = (LDAE.Jetstream)m;
                                    ProcessLogicalDeviceAddedEvent(message);
                                    break;
                                }
                            case "tersosolutions.jetstream.application.sqs.logicaldeviceremovedevent.jetstream":
                                {
                                    LDRE.Jetstream message = (LDRE.Jetstream)m;
                                    ProcessLogicalDeviceRemovedEvent(message);
                                    break;
                                }
                            case "tersosolutions.jetstream.application.sqs.objectevent.jetstream":
                                {
                                    OE.Jetstream message = (OE.Jetstream)m;
                                    ProcessObjectEvent(message);
                                    break;
                                }
                            case "tersosolutions.jetstream.application.sqs.sensorreadingevent.jetstream":
                                {
                                    SRE.Jetstream message = (SRE.Jetstream)m;
                                    ProcessSensorReadingEvent(message);
                                    break;
                                }
                            //case "aws notification - subscription confirmation":
                            //    {
                            //        AE.Jetstream message = (AE.Jetstream)m;
                            //        ProcessSNSControlMessage(message);
                            //        break;
                            //    }
                            default:
                                {
                                    ProcessUnknownMessage(m.ToString());
                                    break;
                                }

                        }
                    }
                    catch (Exception ex)
                    {
                        EventLog.WriteEntry("JetstreamSDK",
                            ex.Message + "\n" + ex.StackTrace);
                    }
                }
            }
        }

        /// <summary>
        /// Method for processing a new <paramref name="aggregateEvent"/>
        /// </summary>
        /// <param name="aggregateEvent">
        /// Deserialized AggregateEvent message in the xsd.exe object model
        /// </param>
        protected virtual void ProcessAggregateEvent(AE.Jetstream aggregateEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="commandCompletionEvent"/>
        /// </summary>
        /// <param name="commandCompletionEvent">
        /// Deserialized CommandCompletionEvent message in the xsd.exe object model
        /// </param>
        protected virtual void ProcessCommandCompletionEvent(CCE.Jetstream commandCompletionEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="commandQueuedEvent"/>
        /// </summary>
        /// <param name="commandQueuedEvent">
        /// Deserialized CommandQueuedEvent message in the xsd.exe object model
        /// </param>
        protected virtual void ProcessCommandQueuedEvent(CQE.Jetstream commandQueuedEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="deviceFailureEvent"/>
        /// </summary>
        /// <param name="deviceFailureEvent">
        /// Deserialized DeviceFailureEvent message in the xsd.exe object model
        /// </param>
        protected virtual void ProcessDeviceFailureEvent(DFE.Jetstream deviceFailureEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="deviceRestoreEvent"/>
        /// </summary>
        /// <param name="deviceRestoreEvent">
        /// Deserialized DeviceRestoreEvent message in the xsd.exe object model
        /// </param>
        protected virtual void ProcessDeviceRestoreEvent(DRE.Jetstream deviceRestoreEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="heartbeatEvent"/>
        /// </summary>
        /// <param name="heartbeatEvent">
        /// Deserialized HeartbeatEvent message in the xsd.exe object model
        /// </param>
        protected virtual void ProcessHeartbeatEvent(HE.Jetstream heartbeatEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="logEnteryEvent"/>
        /// </summary>
        /// <param name="logEntryEvent">
        /// Deserialized LogEntryEvent message in the xsd.exe object model
        /// </param>
        protected virtual void ProcessLogEntryEvent(LEE.Jetstream logEntryEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="logicalDeviceAddedEvent"/>
        /// </summary>
        /// <param name="logicalDeviceAddedEvent">
        /// Deserialized LogicalDeviceAddedEvent in the xsd.exe object model
        /// </param>
        protected virtual void ProcessLogicalDeviceAddedEvent(LDAE.Jetstream logicalDeviceAddedEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="logicalDeviceRemovedEvent"/>
        /// </summary>
        /// <param name="logicalDeviceRemovedEvent">
        /// Deserialized LogicalDeviceRemovedEvent in the xsd.exe object model
        /// </param>
        protected virtual void ProcessLogicalDeviceRemovedEvent(LDRE.Jetstream logicalDeviceRemovedEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="objectEvent"/>
        /// </summary>
        /// <param name="objectEvent">
        /// Deserialized ObjectEvent in the xsd.exe object model
        /// </param>
        protected virtual void ProcessObjectEvent(OE.Jetstream objectEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="sensorReadingEvent"/>
        /// </summary>
        /// <param name="sensorReadingEvent">
        /// Deserialized SensorReadingEvent in the xsd.exe object model
        /// </param>
        protected virtual void ProcessSensorReadingEvent(SRE.Jetstream sensorReadingEvent) { }

        /// <summary>
        /// Method for processing SNS control messages. IE: SubscriptionConfirmation
        /// </summary>
        /// <param name="message">
        /// 
        /// </param>
        protected virtual void ProcessSNSControlMessage(String message) { }

        /// <summary>
        /// Method for processing unknown messages.
        /// </summary>
        /// <param name="message">
        /// The unknown message body
        /// </param>
        protected virtual void ProcessUnknownMessage(String message) { }

        /// <summary>
        /// Deserializes a Jetstream message from a string into a xsd.exe message object graph
        /// </summary>
        /// <typeparam name="T">The type of message</typeparam>
        /// <param name="message">The string xml version of the message</param>
        /// <returns>the <paramref name="message"/> deserialized into <paramref name="T"/></returns>
        private static T Deserialize<T>(String message) where T : new()
        {
            StringReader reader = new StringReader(message);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(reader);
        }

        /// <summary>
        /// Unzips a byte array with the gzip algorithm
        /// </summary>
        /// <param name="data">zipped byte array</param>
        /// <returns>
        /// unzipped byte array
        /// </returns>
        private static byte[] Unzip(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            {
                using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                {
                    using (var resultStream = new MemoryStream())
                    {
                        var buffer = new byte[4096];
                        int read;

                        while ((read = zipStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            resultStream.Write(buffer, 0, read);
                        }

                        return resultStream.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// Method for converting a .net DateTime struct to its equivilent epoch time
        /// </summary>
        /// <param name="dt">The .net DateTime to convert</param>
        /// <returns>
        /// The epoch time in milliseconds
        /// </returns>
        private static long ToEpochTimeInMilliseconds(DateTime dt)
        {
            DateTime temp = (dt.Kind == DateTimeKind.Local) ? dt.ToUniversalTime() : dt;
            return Convert.ToInt64(temp.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
        }

        #endregion
    }
}
