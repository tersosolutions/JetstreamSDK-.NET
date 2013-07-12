﻿/*
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
using Newtonsoft.Json;
using AE = TersoSolutions.Jetstream.SDK.Application.Messages.AggregateEvent;
using CCE = TersoSolutions.Jetstream.SDK.Application.Messages.CommandCompletionEvent;
using CQE = TersoSolutions.Jetstream.SDK.Application.Messages.CommandQueuedEvent;
using DFE = TersoSolutions.Jetstream.SDK.Application.Messages.DeviceFailureEvent;
using DRE = TersoSolutions.Jetstream.SDK.Application.Messages.DeviceRestoreEvent;
using HE = TersoSolutions.Jetstream.SDK.Application.Messages.HeartbeatEvent;
using LDAE = TersoSolutions.Jetstream.SDK.Application.Messages.LogicalDeviceAddedEvent;
using LDRE = TersoSolutions.Jetstream.SDK.Application.Messages.LogicalDeviceRemovedEvent;
using LEE = TersoSolutions.Jetstream.SDK.Application.Messages.LogEntryEvent;
using OE = TersoSolutions.Jetstream.SDK.Application.Model.ObjectEvent;
using SRE = TersoSolutions.Jetstream.SDK.Application.Messages.SensorReadingEvent;
using TersoSolutions.Jetstream.SDK.Application.Model;
using TersoSolutions.Jetstream.SDK.Application.Messages;

namespace TersoSolutions.Jetstream.SDK.Application.Events
{
    /// <summary>
    /// An abstract windows service class that pops messages from Jetstream Ground.
    /// </summary>
    /// <remarks>Author Mark Neustadt</remarks>
    public abstract class JetstreamEventService : ServiceBase
    {

        #region Data

        private Object _newWindowLock = new object();
        private Timer _windowTimer;
        private Object _windowLock = new object();
        private CancellationTokenSource _cts;
        private Object _setLock = new object();
        private SortedSet<JetstreamEvent> _set = new SortedSet<JetstreamEvent>(new JetstreamEventTimeStampComparer()); //use a  Red-Black tree for the producer-consumer data store

        private volatile bool _isWindowing = false;
        private event EventHandler<NewWindowEventArgs> NewWindow;

        #endregion

        #region Properties

        /// <summary>
        /// Get to determine if Gzip is enabled for the application.  
        /// Defaults to false.  Settable by putting an appSettings boolean 
        /// with key GzipEnabled.
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
        /// The TimeSpan that all results Jetstream Ground are windowed 
        /// and sorted to before firing the NewWindow event.
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
        /// The Jetstream Ground subscribe url.
        /// </summary>
        /// 
        protected String JetstreamUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["JetstreamUrl"];
            }
        }

        /// <summary>
        /// An access key used to authenticate to Jetstream Ground.
        /// </summary>
        /// 
        protected String UserAccessKey
        {
            get
            {
                return ConfigurationManager.AppSettings["UserAccessKey"];
            }
        }



        /// <summary>
        /// Indicates if this Events service is currently polling/windowing events.
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
        /// override the OnStart for the windows service to hook the NewWindow event.
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
        /// Override of the WindowsService Stop.
        /// </summary>
        protected override void OnStop()
        {
            // stop all of the background processing
            StopProcesses();

            base.OnStop();
        }

        /// <summary>
        /// Override of the Pause.
        /// </summary>
        protected override void OnPause()
        {
            // stop all of the background processing
            StopProcesses();

            base.OnPause();
        }

        /// <summary>
        /// Override of the Continue.
        /// </summary>
        protected override void OnContinue()
        {
            // start all of the background processing
            StartProcesses();

            base.OnContinue();
        }

        /// <summary>
        /// Override of the Power Event.
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
        /// Override of the Shutdown.
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
        /// Task for Receiving messages from Jetstream Ground.
        /// </summary>
        private string ReceiveTask(CancellationToken ct)
        {
            try
            {
                JetstreamServiceClient client = new JetstreamServiceClient(this.JetstreamUrl, this.UserAccessKey);
                GetEventsRequest request = new GetEventsRequest();
                GetEventsResponse response = client.GetEvents(request);
                string currentBatchId = response.BatchId;
                List<TersoSolutions.Jetstream.SDK.Application.Messages.JetstreamEvent> messages = new List<TersoSolutions.Jetstream.SDK.Application.Messages.JetstreamEvent>();
                foreach (TersoSolutions.Jetstream.SDK.Application.Messages.JetstreamEvent message in response.Events)
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
                return currentBatchId;
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("JetstreamSDK",
                    ex.Message + "\n" + ex.StackTrace,
                    EventLogEntryType.Error);
            }
            return string.Empty;
        }

        /// <summary>
        /// Task for deleting messages from Jetstream Ground.
        /// </summary>
        /// <param name="state"></param>
        private void DeleteTask(CancellationToken ct, string batchId)
        {
            try
            {
                JetstreamServiceClient client = new JetstreamServiceClient(this.JetstreamUrl, this.UserAccessKey);
                RemoveEventsRequest request = new RemoveEventsRequest();
                request.BatchId = batchId;
                RemoveEventsResponse response = client.RemoveEvents(request);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("JetstreamSDK",
                    ex.Message + "\n" + ex.StackTrace,
                    EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Callback for windowing the messages ordered by time queued.
        /// </summary>
        /// <param name="state"></param>
        private void WindowCallback(Object state)
        {
            try
            {
                CancellationToken ct = (CancellationToken)state;
                List<JetstreamEvent> messages = new List<JetstreamEvent>();
                string BatchId = String.Empty;

                // syncronize the processing of windows
                if ((!ct.IsCancellationRequested) &
                    (Monitor.TryEnter(_windowLock, 1000)))
                {
                    try
                    {
                        long epochWindowTime = ToEpochTimeInMilliseconds(DateTime.UtcNow.Subtract(this.MessageCheckWindow));
                        BatchId = ReceiveTask(ct);

                        // just do it inline it's less expensive than spinning threads
                        //ReceiveTask(ct, numberOfMessages);

                        // ok so all messages have been Receieved and ordered or no more messages can be popped
                        if (!ct.IsCancellationRequested)
                        {
                            foreach (var message in _set)
                            {
                                messages.Add(message);
                            }

                            // remove duplicates
                            //TODO Find a better way to dedup that doesn't destroy the order of the array
                            IEnumerable<JetstreamEvent> dedupMessages = messages.Distinct(new JetstreamEventEqualityComparer());

                            // distinct doesn't presever order so we need to reorder the window
                            IEnumerable<JetstreamEvent> orderedMessages = dedupMessages.OrderBy(m => m, new JetstreamEventTimeStampComparer());

                            // raise the WindowEvent with the results
                            if (!ct.IsCancellationRequested)
                            {
                                OnNewWindow(orderedMessages);
                            }
                        }

                    }
                    finally
                    {
                        Monitor.Exit(_windowLock);
                    }
                }

                // check if we should delete the messages
                if ((!ct.IsCancellationRequested) && (messages.Count > 0))
                {
                    DeleteTask(ct, BatchId);
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
        /// Starts all of the background processing.
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
        /// Cancels all of the background processing.
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
        /// On* event raising pattern implementation for the NewWindow event.
        /// </summary>
        /// <param name="messages"></param>
        private void OnNewWindow(IEnumerable<JetstreamEvent> messages)
        {
            if (NewWindow != null)
            {
                NewWindow(this, new NewWindowEventArgs(messages));
            }
        }

        /// <summary>
        /// Event handler for the NewWindow event.
        /// </summary>
        /// <param name="sender">Events Service</param>
        /// <param name="e">The NewWindow event args</param>
        private void JetstreamService_NewWindow(object sender, NewWindowEventArgs e)
        {
            // lock so we process all events in order
            lock (_newWindowLock)
            {
                foreach (TersoSolutions.Jetstream.SDK.Application.Messages.JetstreamEvent m in e.Messages)
                {
                    try
                    {
                        Debug.WriteLine("Jetstream Message: " + m.EventType);

                        // now we can deserialize the XML message into the appropriate message
                        switch (m.EventType.Trim().ToLower())
                        {
                            case "aggregateevent":
                                {
                                    AE.Jetstream message = (AE.Jetstream)m;
                                    ProcessAggregateEvent(message);
                                    break;
                                }
                            case "commandcompletionevent":
                                {
                                    CCE.Jetstream message = (CCE.Jetstream)m;
                                    ProcessCommandCompletionEvent(message);
                                    break;
                                }
                            case "commandqueuedevent":
                                {
                                    CQE.Jetstream message = (CQE.Jetstream)m;
                                    ProcessCommandQueuedEvent(message);
                                    break;
                                }
                            case "devicefailureevent":
                                {
                                    DFE.Jetstream message = (DFE.Jetstream)m;
                                    ProcessDeviceFailureEvent(message);
                                    break;
                                }
                            case "devicerestoreevent":
                                {
                                    DRE.Jetstream message = (DRE.Jetstream)m;
                                    ProcessDeviceRestoreEvent(message);
                                    break;
                                }
                            case "heartbeatevent":
                                {
                                    HE.Jetstream message = (HE.Jetstream)m;
                                    ProcessHeartbeatEvent(message);
                                    break;
                                }
                            case "logentryevent":
                                {
                                    LEE.Jetstream message = (LEE.Jetstream)m;
                                    ProcessLogEntryEvent(message);
                                    break;
                                }
                            case "logicaldeviceaddedevent":
                                {
                                    LDAE.Jetstream message = (LDAE.Jetstream)m;
                                    ProcessLogicalDeviceAddedEvent(message);
                                    break;
                                }
                            case "logicaldeviceremovedevent":
                                {
                                    LDRE.Jetstream message = (LDRE.Jetstream)m;
                                    ProcessLogicalDeviceRemovedEvent(message);
                                    break;
                                }
                            case "objectevent":
                                {
                                    OE.Jetstream message = (OE.Jetstream)m;
                                    ProcessObjectEvent(message);
                                    break;
                                }
                            case "sensorreadingevent":
                                {
                                    SRE.Jetstream message = (SRE.Jetstream)m;
                                    ProcessSensorReadingEvent(message);
                                    break;
                                }
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
        /// Deserialized AggregateEvent message in the xsd.exe object model.
        /// </param>
        protected virtual void ProcessAggregateEvent(AE.Jetstream aggregateEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="commandCompletionEvent"/>
        /// </summary>
        /// <param name="commandCompletionEvent">
        /// Deserialized CommandCompletionEvent message in the xsd.exe object model.
        /// </param>
        protected virtual void ProcessCommandCompletionEvent(CCE.Jetstream commandCompletionEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="commandQueuedEvent"/>
        /// </summary>
        /// <param name="commandQueuedEvent">
        /// Deserialized CommandQueuedEvent message in the xsd.exe object model.
        /// </param>
        protected virtual void ProcessCommandQueuedEvent(CQE.Jetstream commandQueuedEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="deviceFailureEvent"/>
        /// </summary>
        /// <param name="deviceFailureEvent">
        /// Deserialized DeviceFailureEvent message in the xsd.exe object model.
        /// </param>
        protected virtual void ProcessDeviceFailureEvent(DFE.Jetstream deviceFailureEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="deviceRestoreEvent"/>
        /// </summary>
        /// <param name="deviceRestoreEvent">
        /// Deserialized DeviceRestoreEvent message in the xsd.exe object model.
        /// </param>
        protected virtual void ProcessDeviceRestoreEvent(DRE.Jetstream deviceRestoreEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="heartbeatEvent"/>
        /// </summary>
        /// <param name="heartbeatEvent">
        /// Deserialized HeartbeatEvent message in the xsd.exe object model.
        /// </param>
        protected virtual void ProcessHeartbeatEvent(HE.Jetstream heartbeatEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="logEnteryEvent"/>
        /// </summary>
        /// <param name="logEntryEvent">
        /// Deserialized LogEntryEvent message in the xsd.exe object model.
        /// </param>
        protected virtual void ProcessLogEntryEvent(LEE.Jetstream logEntryEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="logicalDeviceAddedEvent"/>
        /// </summary>
        /// <param name="logicalDeviceAddedEvent">
        /// Deserialized LogicalDeviceAddedEvent in the xsd.exe object model.
        /// </param>
        protected virtual void ProcessLogicalDeviceAddedEvent(LDAE.Jetstream logicalDeviceAddedEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="logicalDeviceRemovedEvent"/>
        /// </summary>
        /// <param name="logicalDeviceRemovedEvent">
        /// Deserialized LogicalDeviceRemovedEvent in the xsd.exe object model.
        /// </param>
        protected virtual void ProcessLogicalDeviceRemovedEvent(LDRE.Jetstream logicalDeviceRemovedEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="objectEvent"/>
        /// </summary>
        /// <param name="objectEvent">
        /// Deserialized ObjectEvent in the xsd.exe object model.
        /// </param>
        protected virtual void ProcessObjectEvent(OE.Jetstream objectEvent) { }

        /// <summary>
        /// Method for processing a new <paramref name="sensorReadingEvent"/>
        /// </summary>
        /// <param name="sensorReadingEvent">
        /// Deserialized SensorReadingEvent in the xsd.exe object model.
        /// </param>
        protected virtual void ProcessSensorReadingEvent(SRE.Jetstream sensorReadingEvent) { }

        /// <summary>
        /// Method for processing SNS control messages. IE: SubscriptionConfirmation.
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
        /// Deserializes a Jetstream message from a string into a xsd.exe message object graph.
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
        /// Unzips a byte array with the gzip algorithm.
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
        /// Method for converting a .net DateTime struct to its equivilent epoch time.
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
