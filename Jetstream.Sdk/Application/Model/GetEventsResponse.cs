﻿/*
     Copyright 2015 Terso Solutions, Inc.

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
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TersoSolutions.Jetstream.SDK.Application.Messages;
using GE = TersoSolutions.Jetstream.SDK.Application.Model.Deserialized.GetEventsResponse;
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

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// GetEvents returns but does not deque the oldest pending event messages 
    /// for the Jetstream user. No more than 100 messages will be returned 
    /// on any given GetEvents call.
    /// 
    /// Response object for the Jetstream GetEventsResponse endpoint.
    /// </summary>
    public class GetEventsResponse : JetstreamResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<JetstreamEvent> Events
        {
            get
            {
                List<JetstreamEvent> returnList = new List<JetstreamEvent>();

                // load the full body into an xml document
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Body);

                // get an array of the xml elements of the Jetstream/GetEventsResponse/EventList node
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("x", "http://Jetstream.TersoSolutions.com/v1.5/GetEventsResponse");

                XmlNode eventListNode = doc.SelectSingleNode("/x:Jetstream/x:GetEventsResponse/x:EventList", nsmgr);
                XmlNodeList eventNodes = eventListNode.ChildNodes;
                foreach (XmlNode eventNode in eventNodes)
                {
                    string eventDocXML = eventNode.OuterXml;
                    XmlDocument eventDoc = new XmlDocument();
                    eventDoc.LoadXml(eventDocXML);

                    string evtType = eventDoc.DocumentElement.NamespaceURI;
                    evtType = evtType.Substring(evtType.LastIndexOf('/') + 1);

                    // now we can deserialize the XML message into the appropriate message
                    switch (evtType.Trim().ToLower())
                    {
                        case "aggregateevent":
                            {
                                AE.Jetstream evt = Deserialize<AE.Jetstream>(eventDocXML);
                                evt.EventType = evtType.Trim();
                                evt.EventId = evt.Header.EventId;
                                evt.EventTime = evt.Header.ReceivedTime;
                                returnList.Add(evt);
                                break;
                            }
                        case "commandcompletionevent":
                            {
                                CCE.Jetstream evt = Deserialize<CCE.Jetstream>(eventDocXML);
                                evt.EventType = evtType.Trim();
                                evt.EventId = evt.Header.EventId;
                                evt.EventTime = evt.Header.ReceivedTime;
                                returnList.Add(evt);
                                break;
                            }
                        case "commandqueuedevent":
                            {
                                CQE.Jetstream evt = Deserialize<CQE.Jetstream>(eventDocXML);
                                evt.EventType = evtType.Trim();
                                evt.EventId = evt.Header.EventId;
                                evt.EventTime = evt.Header.EventTime;
                                returnList.Add(evt);
                                break;
                            }
                        case "devicefailureevent":
                            {
                                DFE.Jetstream evt = Deserialize<DFE.Jetstream>(eventDocXML);
                                evt.EventType = evtType.Trim();
                                evt.EventId = evt.Header.EventId;
                                evt.EventTime = evt.Header.EventTime;
                                returnList.Add(evt);
                                break;
                            }
                        case "devicerestoreevent":
                            {
                                DRE.Jetstream evt = Deserialize<DRE.Jetstream>(eventDocXML);
                                evt.EventType = evtType.Trim();
                                evt.EventId = evt.Header.EventId;
                                evt.EventTime = evt.Header.EventTime;
                                returnList.Add(evt);
                                break;
                            }
                        case "heartbeatevent":
                            {
                                HE.Jetstream evt = Deserialize<HE.Jetstream>(eventDocXML);
                                evt.EventType = evtType.Trim();
                                evt.EventId = evt.Header.EventId;
                                evt.EventTime = evt.Header.ReceivedTime;
                                returnList.Add(evt);
                                break;
                            }
                        case "logentryevent":
                            {
                                LEE.Jetstream evt = Deserialize<LEE.Jetstream>(eventDocXML);
                                evt.EventType = evtType.Trim();
                                evt.EventId = evt.Header.EventId;
                                evt.EventTime = evt.Header.ReceivedTime;
                                returnList.Add(evt);
                                break;
                            }
                        case "logicaldeviceaddedevent":
                            {
                                LDAE.Jetstream evt = Deserialize<LDAE.Jetstream>(eventDocXML);
                                evt.EventType = evtType.Trim();
                                evt.EventId = evt.Header.EventId;
                                evt.EventTime = evt.Header.EventTime;
                                returnList.Add(evt);
                                break;
                            }
                        case "logicaldeviceremovedevent":
                            {
                                LDRE.Jetstream evt = Deserialize<LDRE.Jetstream>(eventDocXML);
                                evt.EventType = evtType.Trim();
                                evt.EventId = evt.Header.EventId;
                                evt.EventTime = evt.Header.EventTime;
                                returnList.Add(evt);
                                break;
                            }
                        case "objectevent":
                            {
                                OE.Jetstream evt = Deserialize<OE.Jetstream>(eventDocXML);
                                evt.EventType = evtType.Trim();
                                evt.EventId = evt.Header.EventId;
                                evt.EventTime = evt.Header.ReceivedTime;
                                returnList.Add(evt);
                                break;
                            }
                        case "sensorreadingevent":
                            {
                                SRE.Jetstream evt = Deserialize<SRE.Jetstream>(eventDocXML);
                                evt.EventType = evtType.Trim();
                                evt.EventId = evt.Header.EventId;
                                evt.EventTime = evt.Header.ReceivedTime;
                                returnList.Add(evt);
                                break;
                            }
                        default:
                            {
                                break;
                            }

                    }
                }

                return returnList;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BatchId
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Body);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("x", "http://Jetstream.TersoSolutions.com/v1.5/GetEventsResponse");

                XmlNode headerNode = doc.SelectSingleNode("/x:Jetstream/x:Header", nsmgr);
                XmlAttribute batchAttribute = headerNode.Attributes["BatchId"];

                return batchAttribute.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int EventCount
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Body);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("x", "http://Jetstream.TersoSolutions.com/v1.5/GetEventsResponse");

                XmlNode headerNode = doc.SelectSingleNode("/x:Jetstream/x:Header", nsmgr);
                XmlAttribute eventCountAttribute = headerNode.Attributes["Count"];

                return Int32.Parse(eventCountAttribute.Value);
            }
        }

        /// <summary>
        /// Deserializes a Jetstream message from a string into a xsd.exe message object graph
        /// </summary>
        /// <typeparam name="T">The type of message</typeparam>
        /// <param name="message">The string xml version of the message</param>
        /// <returns>the <paramref name="message"/> deserialized into type T</returns>
        private static T Deserialize<T>(string message) where T : new()
        {
            // Dispose of StringReader indirectly via using statement
            using (StringReader reader = new StringReader(message))
            {
                XmlSerializer serializer = new XmlSerializer(typeof (T));
                return (T) serializer.Deserialize(reader);
            }
        }
    }
}
