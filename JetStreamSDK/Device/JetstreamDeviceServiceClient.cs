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
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AE = TersoSolutions.Jetstream.SDK.Device.AggregateEvent;
using CCE = TersoSolutions.Jetstream.SDK.Device.CommandCompletionEvent;
using CR = TersoSolutions.Jetstream.SDK.Device.GetCommandsResponse;
using HE = TersoSolutions.Jetstream.SDK.Device.HeartbeatEvent;
using LE = TersoSolutions.Jetstream.SDK.Device.LogEntryEvent;
using OE = TersoSolutions.Jetstream.SDK.Device.ObjectEvent;
using SE = TersoSolutions.Jetstream.SDK.Device.SensorReadingEvent;

namespace TersoSolutions.Jetstream.SDK.Device
{
    /// <summary>
    /// Jetstream device service client is a proxy to the Jetstream DeviceWebServer
    /// Devices send messages into the DeviceWebServer endpoint
    /// </summary>
    public class JetstreamDeviceServiceClient
    {
        private String _deviceWebServerUrl;
        private String _deviceAccessKey;

        #region Constructor

        /// <summary>
        /// Constructor for the device service client
        /// </summary>
        /// <param name="deviceWebServerUrl"></param>
        /// <param name="deviceAccessKey"></param>
        public JetstreamDeviceServiceClient(
            String deviceWebServerUrl,
            String deviceAccessKey)
        {
            if (String.IsNullOrEmpty(deviceWebServerUrl)) throw new ArgumentNullException("deviceWebServerUrl");
            if (String.IsNullOrEmpty(deviceAccessKey)) throw new ArgumentNullException("deviceAccessKey");

            deviceWebServerUrl = deviceWebServerUrl.Trim();

            // normalize url
            if (!deviceWebServerUrl.EndsWith("?"))
            {
                if (!deviceWebServerUrl.EndsWith("/"))
                {
                    deviceWebServerUrl += "/";
                }
                deviceWebServerUrl += "?";
            }
            // store params for reuse 
            _deviceWebServerUrl = deviceWebServerUrl;
            _deviceAccessKey = deviceAccessKey;
        }
        #endregion

        #region "Send Aggregate Event"

        /// <summary>
        /// Delegate for async sending the AggregateEvent
        /// </summary>
        /// <param name="request"></param>
        public delegate void SendAggregateEventAsync(AE.Jetstream request);
        SendAggregateEventAsync _sendAggregateEventAsync;

        /// <summary>
        /// IAsyncResult pattern to async send the AggregateEvent
        /// </summary>
        /// <param name="request"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginSendAggregateEvent(AE.Jetstream request, AsyncCallback callback, Object state)
        {
            _sendAggregateEventAsync = new SendAggregateEventAsync(SendAggregateEvent);
            return _sendAggregateEventAsync.BeginInvoke(request, callback, state);
          
        }

        public void EndSendAggregateEvent(IAsyncResult result)
        {
            object state = result.AsyncState;
            _sendAggregateEventAsync.EndInvoke(result);
        }

        /// <summary>
        /// Send an aggregate event to the Jetstream Device Webserver
        /// </summary>
        /// <param name="request">The AggregateEvent request</param>
        public void SendAggregateEvent(AE.Jetstream request)
        {
            if (request == null) throw new ArgumentNullException("request");
            
            String message = ChangeDatesToUTC(MessageHelper.SerializeObject( typeof(AE.Jetstream), request), new String[] { "EventTime" }, "http://Jetstream.TersoSolutions.com/v1.0/Device/AggregateEvent");
            SendMessageToJetStream(message);
        }
        #endregion

        #       region "Send Log Entry Event"
        public delegate void SendLogEntryEventAsync(LE.Jetstream request);
        SendLogEntryEventAsync _sendLogEntryEventAsync;
        //public class SendLogEntryEventState
        //{
        //    public object previousState;
        //    public SendLogEntryEventAsyncStub asyncStub;
        //}

        public IAsyncResult BeginSendLogEntryEvent(LE.Jetstream request, AsyncCallback callback, Object state)
        {
            _sendLogEntryEventAsync = new SendLogEntryEventAsync(SendLogEntryEvent);
            //SendLogEntryEventState es = new SendLogEntryEventState();
            //es.previousState = state;
            //es.asyncStub = stub;
            //return stub.BeginInvoke(request, callback, es);

            return _sendLogEntryEventAsync.BeginInvoke(request, callback, state);
        }

        public void EndSendLogEntryEvent(IAsyncResult result)
        {
            //SendLogEntryEventState ms = (SendLogEntryEventState)result.AsyncState;
            //ms.asyncStub.EndInvoke(result);
            //throw new NotImplementedException();
            object state = result.AsyncState;
            _sendLogEntryEventAsync.EndInvoke(result);
        }

        /// <summary>
        /// Send a LogEntryEvent to the Jetstream Device Webserver
        /// </summary>
        /// <param name="request">The LogEntryEvent request</param>
        public void SendLogEntryEvent(LE.Jetstream request)
        {
            if (request == null) throw new ArgumentNullException("request");

            String message = ChangeDatesToUTC(MessageHelper.SerializeObject(typeof(LE.Jetstream), request), new String[] { "EventTime", "LogTime" }, "http://Jetstream.TersoSolutions.com/v1.0/Device/LogEntryEvent");

            SendMessageToJetStream(message);
        }

        #endregion

        #region "Send GetCommand Request"

        public delegate CR.Jetstream SendGetCommandEventAsync(GetCommandsRequest request);
        SendGetCommandEventAsync _sendGetCommandEventAsync;

        public IAsyncResult BeginSendGetCommandsRequest(GetCommandsRequest request, AsyncCallback callback, Object state)
        {
            this._sendGetCommandEventAsync = new SendGetCommandEventAsync(SendGetCommandsRequest);

            return this._sendGetCommandEventAsync.BeginInvoke(request, callback, state);
        }

        public CR.Jetstream EndSendGetCommandsRequest(IAsyncResult result)
        {
            return this._sendGetCommandEventAsync.EndInvoke(result);
        }

        /// <summary>
        /// Send a GetCommandsRequest to the Jetstream Device Webserver
        /// </summary>
        /// <param name="request">The GetCommandsRequest request</param>
        public CR.Jetstream SendGetCommandsRequest(GetCommandsRequest request)
        {

            if (request == null) throw new ArgumentNullException("request");

            String responseXmlString = SendMessageToJetStream("GetCommands", string.Empty);

            CR.Jetstream getCommandResponse = null;

            XmlSerializer s = new XmlSerializer(typeof(CR.Jetstream));
            getCommandResponse = (CR.Jetstream)s.Deserialize(new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(responseXmlString))));

            return getCommandResponse;
        }

        #endregion

        #region "Send Heartbeat Event"
        
        public delegate void SendHeartbeatEventAsync(HE.Jetstream request);
        SendHeartbeatEventAsync _sendHeartbeatEventAsync;

        public IAsyncResult BeginSendHeartbeatEvent(HE.Jetstream request, AsyncCallback callback, Object state)
        {
            this._sendHeartbeatEventAsync = new SendHeartbeatEventAsync(SendHeartbeatEvent);
            return this._sendHeartbeatEventAsync.BeginInvoke(request, callback, state);
        }

        public void EndSendHeartbeatEvent(IAsyncResult result)
        {
            this._sendHeartbeatEventAsync.EndInvoke(result);
        }

        /// <summary>
        /// Send a HeartbeatEvent to the Jetstream Device Webserver
        /// </summary>
        /// <param name="request">The HeartbeatEvent</param>
        public void SendHeartbeatEvent(HE.Jetstream request)
        {
            if (request == null) throw new ArgumentNullException("request");

            String message = ChangeDatesToUTC(MessageHelper.SerializeObject(typeof(HE.Jetstream), request), new String[] { "EventTime" }, "http://Jetstream.TersoSolutions.com/v1.0/Device/HeartbeatEvent");
            SendMessageToJetStream(message);
        }
        #endregion

        #region "Send Command Completion Event"
        
        public delegate void SendCommandCompletionEventAsync(CCE.Jetstream request);
        SendCommandCompletionEventAsync _sendCommandCompletionEventAsync;

        public IAsyncResult BeginSendCommandCompletionEvent(CCE.Jetstream request, AsyncCallback callback, Object state)
        {
            this._sendCommandCompletionEventAsync = new SendCommandCompletionEventAsync(SendCommandCompletionEvent);
            return this._sendCommandCompletionEventAsync.BeginInvoke(request, callback, state);
        }

        public void EndSendCommandCompletionEvent(IAsyncResult result)
        {
            _sendCommandCompletionEventAsync.EndInvoke(result);
        }

        /// <summary>
        /// Send a CommandCompletionEvent to the Jetstream Device Webserver
        /// </summary>
        /// <param name="request">The CommandCompletionEvent</param>
        public void SendCommandCompletionEvent(CCE.Jetstream request)
        {
            if (request == null) throw new ArgumentNullException("request");

            String message = ChangeDatesToUTC(MessageHelper.SerializeObject(typeof(CCE.Jetstream), request), new String[] { "EventTime" }, "http://Jetstream.TersoSolutions.com/v1.0/Device/CommandCompletionEvent");
            SendMessageToJetStream(message);
        }
        #endregion

        #region "Send Object Event"

        public delegate void SendObjectEventAsync(OE.Jetstream request);
        SendObjectEventAsync _sendObjectEventAsync;
        
        public IAsyncResult BeginSendObjectEvent(OE.Jetstream request, AsyncCallback callback, Object state)
        {
            this._sendObjectEventAsync = new SendObjectEventAsync(SendObjectEvent);
            return this._sendObjectEventAsync.BeginInvoke(request, callback, state);
        }

        public void EndSendObjectEvent(IAsyncResult result)
        {
            _sendObjectEventAsync.EndInvoke(result);
        }

        /// <summary>
        /// Send an ObjectEvent to the Jestream Device Webserver
        /// </summary>
        /// <param name="request">The ObjectEvent</param>
        public void SendObjectEvent(OE.Jetstream request)
        {
            if (request == null) throw new ArgumentNullException("request");

            String message = ChangeDatesToUTC(MessageHelper.SerializeObject(typeof(OE.Jetstream), request), new String[] { "EventTime" }, "http://Jetstream.TersoSolutions.com/v1.0/Device/ObjectEvent");
            SendMessageToJetStream(message);
        }
        #endregion

        #region "Send Sensor Reading Event"

        public delegate void SendSensorReadingEventAsync(SE.Jetstream request);
        SendSensorReadingEventAsync _sendSensorReadingEventAsync;

        public IAsyncResult BeginSendSensorReadingEvent(SE.Jetstream request, AsyncCallback callback, Object state)
        {
            this._sendSensorReadingEventAsync = new SendSensorReadingEventAsync(SendSensorReadingEvent);
            return this._sendSensorReadingEventAsync.BeginInvoke(request, callback, state);
        }

        public void EndSendSensorReadingEvent(IAsyncResult result)
        {
            _sendSensorReadingEventAsync.EndInvoke(result);
        }

        /// <summary>
        /// Send a SensorReadingEvent ot the Jetstream Device Webserver
        /// </summary>
        /// <param name="request">The SensorReadingEvent</param>
        public void SendSensorReadingEvent(SE.Jetstream request)
        {
            if (request == null) throw new ArgumentNullException("request");

            String message = ChangeDatesToUTC(MessageHelper.SerializeObject(typeof(SE.Jetstream), request), new String[] { "EventTime", "ReadingTime" }, "http://Jetstream.TersoSolutions.com/v1.0/Device/SensorReadingEvent");
            SendMessageToJetStream(message);
        }
        #endregion

        #region "Send Get DateTime Request"

        public delegate DateTime SendGetDateTimeRequesttAsync();
        SendGetDateTimeRequesttAsync _sendGetDateTimeRequesttAsync;

        public IAsyncResult BeginSendGetDateTimeRequest(AsyncCallback callback, Object state)
        {
            this._sendGetDateTimeRequesttAsync = new SendGetDateTimeRequesttAsync(SendGetDateTimeRequest);
            return this._sendGetDateTimeRequesttAsync.BeginInvoke(callback, state);
        }

        public DateTime EndSendGetDateTimeRequest(IAsyncResult result)
        {
            return this._sendGetDateTimeRequesttAsync.EndInvoke(result);
        }

        /// <summary>
        /// Send a SendGetDateTimeRequest to the Jetstream Device Webserver
        /// </summary>
        /// <returns>returns DeviceServer DateTime</returns>
        public DateTime SendGetDateTimeRequest()
        {
            String responseDateString = SendMessageToJetStream("GetDateTime", string.Empty);

            DateTime dateTime = DateTime.Parse(responseDateString).ToUniversalTime();

            return dateTime;
        }
        #endregion

        private string SendMessageToJetStream(String message)
        {
            // send request to the Jetstream DeviceWebServer endpoint
            String responseString = MessageHelper.SendToJetStream(String.Concat(new String[] { _deviceWebServerUrl, "AccessKey=", _deviceAccessKey }),
                message);

            return responseString;
        }

        private string SendMessageToJetStream(string commandName, String message)
        {
            // send request to the Jetstream DeviceWebServer endpoint

            string urlWithCmd = AddCmdToUrl(_deviceWebServerUrl, commandName);

            String responseString = MessageHelper.SendToJetStream(String.Concat(new String[] { urlWithCmd, "AccessKey=", _deviceAccessKey }),
                message);

            return responseString;
        }

        private string AddCmdToUrl(string actualUrl, string commandName)
        {
            string exp = "/?";
            if (!actualUrl.EndsWith(exp))
                throw new Exception("Invalid base uri");

            string newUrl = actualUrl.Substring(0, actualUrl.Length - exp.Length);
            newUrl = string.Concat(new string[] { newUrl, "/", commandName, "?" });

            return newUrl;
        }

        private static String ChangeDatesToUTC(String message, String[] dateTimeAttributes, String nameSpace)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new XmlTextReader(new StringReader(message)));
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("x", nameSpace);
            XmlNodeList nodeList = doc.SelectNodes("//x:Jetstream//@*", nsmgr);

            foreach (XmlNode n in nodeList)
            {
                if (dateTimeAttributes.Contains(n.LocalName))
                {
                    n.InnerText = DateTime.Parse(n.InnerText).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
                }
            }

            return doc.OuterXml;
        }
    }
}
