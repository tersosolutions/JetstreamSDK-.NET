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
using Amazon.SQS;
using Amazon.SQS.Model;
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
using OE = TersoSolutions.Jetstream.SDK.Application.Messages.ObjectEvent;
using SRE = TersoSolutions.Jetstream.SDK.Application.Messages.SensorReadingEvent;

namespace TersoSolutions.Jetstream.SDK.Application.SQS
{
    /// <summary>
    /// abstract windows service class that pops messages from a SQS queue that is a subscriber of a Jetstream SNS topic
    /// </summary>
    /// <remarks>Author Mike Lohmeie</remarks>
    public abstract class JetstreamSQSService : ServiceBase
    {
        
        #region Data

        private Object _newWindowLock = new object();
        private Timer _windowTimer;
        private Object _windowLock = new object();
        private CancellationTokenSource _cts;
        private Object _setLock = new object();
        private SortedSet<Message> _set = new SortedSet<Message>(new MessageSentTimeStampComparer()); //use a  Red-Black tree for the producer-consumer data store
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
        protected TimeSpan SQSWindow
        {
            get
            {
                TimeSpan result;
                if ((ConfigurationManager.AppSettings["SQSWindow"] != null) &&
                    (TimeSpan.TryParse(ConfigurationManager.AppSettings["SQSWindow"], out result)))
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
        protected String QueueUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["QueueUrl"];
            }
        }

        /// <summary>
        /// The Amazon access key to access SQS with
        /// </summary>
        protected String AWSAccessKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AWSAccessKey"];
            }
        }

        /// <summary>
        /// The Amazon secret access key to access SQS with
        /// </summary>
        protected String AWSSecretAccessKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AWSSecretAccessKey"];
            }
        }

        /// <summary>
        /// The AWS SQS url to use to access the queue
        /// </summary>
        protected String AWSSQSServiceUrl
        {
            get
            {
                if (ConfigurationManager.AppSettings["AWSSQSServiceUrl"] != null)
                {
                    return ConfigurationManager.AppSettings["AWSSQSServiceUrl"];
                }
                else
                {
                    // default to the us-east1 region url
                    return c_DEFAULTAWSSQSSERVICEURL;
                }
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
            if (String.IsNullOrEmpty(this.AWSAccessKey)) throw new ConfigurationErrorsException("There is no AWSAccesskey in the appSettings");
            if (String.IsNullOrEmpty(this.AWSSecretAccessKey)) throw new ConfigurationErrorsException("There is no AWSSecretAccessKey in the appSettings");
            if (String.IsNullOrEmpty(this.QueueUrl)) throw new ConfigurationErrorsException("There is no QueueUrl in the appSettings");

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
                // build the AWS SQS proxy client
                using (AmazonSQSClient client = new AmazonSQSClient(
                    this.AWSAccessKey, this.AWSSecretAccessKey,
                    new AmazonSQSConfig()
                    {
                        ServiceURL = this.AWSSQSServiceUrl,
                        MaxErrorRetry = 10
                    }))
                {
                    // build the request message
                    ReceiveMessageRequest request = new ReceiveMessageRequest();
                    request.AttributeName.Add("SentTimestamp");
                    request.MaxNumberOfMessages = 10;
                    request.QueueUrl = this.QueueUrl;
                    ReceiveMessageResponse response;
                    List<Message> messages = new List<Message>();
                    do
                    {
                        response = client.ReceiveMessage(request);
                        messages.AddRange(response.ReceiveMessageResult.Message);
                    }
                    while ((response.ReceiveMessageResult.Message.Count != 0) &
                           (messages.Count < numberOfMessages) &
                           (!ct.IsCancellationRequested));

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
        private void DeleteTask(CancellationToken ct, List<Message> messages, Tuple<int, int> range)
        {
            try
            {
                using (AmazonSQSClient client = new AmazonSQSClient(
                        this.AWSAccessKey, this.AWSSecretAccessKey,
                        new AmazonSQSConfig()
                        {
                            ServiceURL = this.AWSSQSServiceUrl,
                            MaxErrorRetry = 10
                        }))
                {
                    DeleteMessageBatchRequest deleteRequest = new DeleteMessageBatchRequest();
                    deleteRequest.QueueUrl = this.QueueUrl;
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        deleteRequest.Entries.Add(new DeleteMessageBatchRequestEntry()
                        {
                            Id = messages[i].MessageId,
                            ReceiptHandle = messages[i].ReceiptHandle
                        });
                        // send 10x or the remainder of the set
                        if ((deleteRequest.Entries.Count == 10) |
                            (i == range.Item2 - 1))
                        {
                            if (ct.IsCancellationRequested)
                            {
                                break;
                            }
                            client.DeleteMessageBatch(deleteRequest);
                            deleteRequest.Entries.Clear();
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
        /// Callback for windowing the SQS messages ordered by time queued
        /// </summary>
        /// <param name="state"></param>
        private void WindowCallback(Object state)
        {
            try
            {
                CancellationToken ct = (CancellationToken)state;
                List<Message> messages = new List<Message>();

                // syncronize the processing of windows
                if ((!ct.IsCancellationRequested) &
                    (Monitor.TryEnter(_windowLock, 1000)))
                {
                    try
                    {
                        long epochWindowTime = ToEpochTimeInMilliseconds(DateTime.UtcNow.Subtract(this.SQSWindow));
                        int numberOfMessages = 0;

                        // 1st query to determine the # of Messages available in the queue
                        using (AmazonSQSClient client = new AmazonSQSClient(
                            this.AWSAccessKey, this.AWSSecretAccessKey,
                            new AmazonSQSConfig() { ServiceURL = this.AWSSQSServiceUrl }))
                        {
                            // get the NumberOfMessages to optimize how to Receive all of the messages from the queue
                            GetQueueAttributesRequest attributesRequest = new GetQueueAttributesRequest();
                            attributesRequest.QueueUrl = this.QueueUrl;
                            attributesRequest.AttributeName.Add("ApproximateNumberOfMessages");
                            numberOfMessages = client.GetQueueAttributes(attributesRequest).GetQueueAttributesResult.ApproximateNumberOfMessages;
                        }

                        // determine if we need to Receive messages from the Queue
                        if (numberOfMessages > 0)
                        {
                            if (numberOfMessages < 10)
                            {
                                // just do it inline it's less expensive than spinning threads
                                ReceiveTask(ct, numberOfMessages);
                            }
                            else
                            {
                                // use the default partitioner to determine the number of tasks
                                // TODO Create a custom partitioner that takes into account the batch x 10 of SQS
                                Parallel.ForEach(Partitioner.Create(0, numberOfMessages),
                                    (range) => ReceiveTask(ct, range.Item2 - range.Item1));
                            }
                        }

                        // ok so all messages have been Receieved and ordered or no more messages can be popped
                        if (!ct.IsCancellationRequested)
                        {
                            // get all messages less than the window time from the rb tree
                            // probably don't have to lock on _setLock because of the _windowLock but let's be safe
                            lock (_setLock)
                            {
                                messages.AddRange(_set.Where((m) => long.Parse(m.Attribute[0].Value) < epochWindowTime));
                                _set.RemoveWhere((m) => long.Parse(m.Attribute[0].Value) < epochWindowTime);
                            }

                            // remove duplicates
                            //TODO Find a better way to dedup that doesn't destroy the order of the array
                            IEnumerable<Message> dedupMessages = messages.Distinct(new MessageEqualityComparer());

                            // distinct doesn't presever order so we need to reorder the window
                            IEnumerable<Message> orderedMessages = dedupMessages.OrderBy(m => m, new MessageSentTimeStampComparer());

                            // raise the WindowEvent with the results
                            if (!ct.IsCancellationRequested)
                            {
                                OnNewWindow(dedupMessages);
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
                    if (messages.Count < 10)
                    {
                        DeleteTask(ct, messages, new Tuple<int, int>(0, messages.Count));
                    }
                    else
                    {
                        // use the default partitioner to determine the number of tasks
                        // TODO Create a custom partitioner that takes into account the batch x 10 of SQS
                        Parallel.ForEach(Partitioner.Create(0, messages.Count),
                            (range) => DeleteTask(ct, messages, range));
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
        /// Starts all of the background processing
        /// </summary>
        private void StartProcesses()
        {
            // create a new cancellationtoken souce
            _cts = new CancellationTokenSource();

            // start the window timer
            _windowTimer = new Timer(new TimerCallback(WindowCallback),
                _cts.Token, 0, Convert.ToInt64(this.SQSWindow.TotalMilliseconds));

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
        private void OnNewWindow(IEnumerable<Message> messages)
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
                foreach (Message m in e.Messages)
                {
                    try
                    {
                        Debug.WriteLine("SQS Message.Body: " + m.Body);

                        // AWS sends base64 encoded bodys for legacy accounts and non base 64 encoded bodys for new accounts
                        // therefore we check to see if the message body is JSON by checking the first char
                        String messageBody = m.Body;
                        if ((!m.Body.StartsWith("<")) &
                            (!m.Body.StartsWith("{")))
                        {
                            messageBody = Encoding.UTF8.GetString(Convert.FromBase64String(m.Body));
                            Debug.WriteLine("SQS Message.Body deBase64: " + messageBody);
                        }


                        // use Newtonsoft.json to parse the message because the JSON serializer from MS doesn't work
                        AmazonSNSMessage b = JsonConvert.DeserializeObject<AmazonSNSMessage>(messageBody);

                        // unzip the message if gzip is enabled for my application
                        String message;
                        if (GzipEnabled)
                        {
                            Byte[] unzipped = Unzip(Convert.FromBase64String(b.Message));
                            message = Encoding.UTF8.GetString(unzipped);
                        }
                        else
                        {
                            message = b.Message;
                        }

                        Debug.WriteLine("Jetstream Message: " + message);

                        // now we can deserialize the XML message into the appropriate message
                        switch (b.Subject.Trim().ToLower())
                        {
                            case "aggregateevent":
                                {
                                    ProcessAggregateEvent(Deserialize<AE.Jetstream>(message));
                                    break;
                                }
                            case "commandcompletionevent":
                                {
                                    ProcessCommandCompletionEvent(Deserialize<CCE.Jetstream>(message));
                                    break;
                                }
                            case "commandqueuedevent":
                                {
                                    ProcessCommandQueuedEvent(Deserialize<CQE.Jetstream>(message));
                                    break;
                                }
                            case "devicefailureevent":
                                {
                                    ProcessDeviceFailureEvent(Deserialize<DFE.Jetstream>(message));
                                    break;
                                }
                            case "devicerestoreevent":
                                {
                                    ProcessDeviceRestoreEvent(Deserialize<DRE.Jetstream>(message));
                                    break;
                                }
                            case "heartbeatevent":
                                {
                                    ProcessHeartbeatEvent(Deserialize<HE.Jetstream>(message));
                                    break;
                                }
                            case "logentryevent":
                                {
                                    ProcessLogEntryEvent(Deserialize<LEE.Jetstream>(message));
                                    break;
                                }
                            case "logicaldeviceaddedevent":
                                {
                                    ProcessLogicalDeviceAddedEvent(Deserialize<LDAE.Jetstream>(message));
                                    break;
                                }
                            case "logicaldeviceremovedevent":
                                {
                                    ProcessLogicalDeviceRemovedEvent(Deserialize<LDRE.Jetstream>(message));
                                    break;
                                }
                            case "objectevent":
                                {
                                    ProcessObjectEvent(Deserialize<OE.Jetstream>(message));
                                    break;
                                }
                            case "sensorreadingevent":
                                {
                                    ProcessSensorReadingEvent(Deserialize<SRE.Jetstream>(message));
                                    break;
                                }
                            case "aws notification - subscription confirmation":
                                {
                                    ProcessSNSControlMessage(message);
                                    break;
                                }
                            default:
                                {
                                    ProcessUnknownMessage(message);
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
        protected virtual void ProcessSNSControlMessage(String message) {}

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
        private static T Deserialize<T>(String message) where T: new()
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
