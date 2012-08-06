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
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using TersoSolutions.AWSUtilities.SQS;
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

namespace TersoSolutions.Jetstream.Application.SQS
{
    /// <summary>
    /// abstract windows service class that pops messages from a SQS queue that is a subscriber of a Jetstream SNS topic
    /// </summary>
    /// <remarks>Author Mike Lohmeie</remarks>
    public abstract class JetstreamSQSService : SQSService
    {

        #region Data

        private static Object _newWindowLock = new object();

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

        #endregion

        #region Service Events

        /// <summary>
        /// override the OnStart for the windows service to hook the NewWindow event
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            // hook an event handler to the NewWindow event
            this.NewWindow += new System.EventHandler<NewWindowEventArgs>(JetstreamService_NewWindow);

            // must call the base otherwise the windowing logic will never fire
            base.OnStart(args);
        }

        #endregion

        #region Methods

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
                        // use Newtonsoft.json to parse the message because the JSON serializer from MS doesn't work
                        AmazonSNSMessage b = JsonConvert.DeserializeObject<AmazonSNSMessage>(m.Body);

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
                        EventLog.WriteEntry(ex.GetType().Name, ex.Message + "\n" + ex.StackTrace);
                    }
                }
            }
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
        /// helper to deserializer a Jetstream message from a string into a xsd.exe message object graph
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

        #endregion
    }
}
