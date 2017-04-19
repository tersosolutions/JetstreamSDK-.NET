using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TersoSolutions.Jetstream.SDK.Device;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TersoSolutions.Jetstream.SDK.Application.Messages.AggregateEvent;
using TersoSolutions.Jetstream.SDK.Device.AggregateEvent;
// using Jetstream = TersoSolutions.Jetstream.SDK.Device.AggregateEvent.Jetstream;
using JetstreamAggregateEvent = TersoSolutions.Jetstream.SDK.Device.AggregateEvent.JetstreamAggregateEvent;
using AE = TersoSolutions.Jetstream.SDK.Device.AggregateEvent;
using CCE = TersoSolutions.Jetstream.SDK.Device.CommandCompletionEvent;
using CR = TersoSolutions.Jetstream.SDK.Device.GetCommandsRequest;
using HE = TersoSolutions.Jetstream.SDK.Device.HeartbeatEvent;
using LE = TersoSolutions.Jetstream.SDK.Device.LogEntryEvent;
using OE = TersoSolutions.Jetstream.SDK.Device.ObjectEvent;
using SE = TersoSolutions.Jetstream.SDK.Device.SensorReadingEvent;


namespace JetstreamDeviceServiceClientTests
{
    [TestClass()]
    [ExcludeFromCodeCoverage]
    public class JetstreamDeviceServiceClientTests
    {
        #region Privates

        private readonly JetstreamDeviceServiceClient _client = new JetstreamDeviceServiceClient("http://localhost/devicewebserver/", "059b7ac1-64a6-431d-89ca-502f3507610f");
        private IAsyncResult _result;

        #endregion

        [TestMethod()]
        public void JetstreamDeviceServiceClientTest()
        {
            JetstreamDeviceServiceClient client = new JetstreamDeviceServiceClient("http://fake", "asdf");
        }

        [TestMethod()]
        public void BeginSendAggregateEventTest()
        {
            // assemble
            AE.Jetstream jetstream = new AE.Jetstream();

            AsyncCallback callback = new AsyncCallback(_client.EndSendAggregateEvent);
            Object state = new object();

            // act 
            _result = _client.BeginSendAggregateEvent(jetstream, callback, state);


            // assert
            Assert.IsTrue(!_result.CompletedSynchronously);
            Assert.IsTrue(!_result.IsCompleted);

        }

        [TestMethod()]
        public void EndSendAggregateEventTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void SendAggregateEventTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void BeginSendLogEntryEventTest()
        {
            //throw new AssertInconclusiveException();
            // assemble
            LE.Jetstream jetstream = new LE.Jetstream();

            AsyncCallback callback = new AsyncCallback(_client.EndSendLogEntryEvent);
            Object state = new object();

            // act
            _result = _client.BeginSendLogEntryEvent(jetstream, callback, state);

            // assert
            Assert.IsTrue(!_result.IsCompleted);
            Assert.IsTrue(!_result.CompletedSynchronously);
        }

        [TestMethod()]
        public void EndSendLogEntryEventTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void SendLogEntryEventTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void BeginSendGetCommandsRequestTest()
        {
            throw new AssertInconclusiveException();
            //// assemble
            //GetCommandsRequest request = new GetCommandsRequest();

            //AsyncCallback callback = new AsyncCallback();
            //Object state = new object();

            //// act
            //_result = _client.BeginSendGetCommandsRequest(request, callback, state);

            //// assert
            //Assert.IsTrue(!_result.IsCompleted);
            //Assert.IsTrue(!_result.CompletedSynchronously);
        }

        [TestMethod()]
        public void EndSendGetCommandsRequestTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void SendGetCommandsRequestTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void BeginSendHeartbeatEventTest()
        {
            //throw new AssertInconclusiveException();
            // assemble
            HE.Jetstream request = new HE.Jetstream();

            AsyncCallback callback = new AsyncCallback(_client.EndSendHeartbeatEvent);
            Object state = new object();

            // act
            _result = _client.BeginSendHeartbeatEvent(request, callback, state);

            // assert
            Assert.IsTrue(!_result.IsCompleted);
            Assert.IsTrue(!_result.CompletedSynchronously);
        }

        [TestMethod()]
        public void EndSendHeartbeatEventTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void SendHeartbeatEventTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void BeginSendCommandCompletionEventTest()
        {
            //throw new AssertInconclusiveException();
            // assemble
            CCE.Jetstream request = new CCE.Jetstream();

            AsyncCallback callback = new AsyncCallback(_client.EndSendCommandCompletionEvent);
            Object state = new object();

            // act
            _result = _client.BeginSendCommandCompletionEvent(request, callback, state);

            // assert
            Assert.IsTrue(!_result.IsCompleted);
            Assert.IsTrue(!_result.CompletedSynchronously);
        }

        [TestMethod()]
        public void EndSendCommandCompletionEventTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void SendCommandCompletionEventTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void BeginSendObjectEventTest()
        {
            //throw new AssertInconclusiveException();
            // assemble
            OE.Jetstream request = new OE.Jetstream();

            AsyncCallback callback = new AsyncCallback(_client.EndSendLogEntryEvent);
            Object state = new object();

            // act
            _result = _client.BeginSendObjectEvent(request, callback, state);

            // assert
            Assert.IsTrue(!_result.IsCompleted);
            Assert.IsTrue(!_result.CompletedSynchronously);
        }

        [TestMethod()]
        public void EndSendObjectEventTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void SendObjectEventTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void BeginSendSensorReadingEventTest()
        {
            //throw new AssertInconclusiveException();
            // assemble
            SE.Jetstream request = new SE.Jetstream();

            AsyncCallback callback = new AsyncCallback(_client.EndSendLogEntryEvent);
            Object state = new object();

            // act
            _result = _client.BeginSendSensorReadingEvent(request, callback, state);

            // assert
            Assert.IsTrue(!_result.IsCompleted);
            Assert.IsTrue(!_result.CompletedSynchronously);
        }

        [TestMethod()]
        public void EndSendSensorReadingEventTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void SendSensorReadingEventTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void BeginSendGetDateTimeRequestTest()
        {
            throw new AssertInconclusiveException();
            //// assemble
            //HE.Jetstream request = new HE.Jetstream();

            //AsyncCallback callback = new AsyncCallback();
            //Object state = new object();

            //// act
            //_result = _client.BeginSendGetDateTimeRequest(callback, state);

            //// assert
            //Assert.IsTrue(!_result.IsCompleted);
            //Assert.IsTrue(!_result.CompletedSynchronously);
        }

        [TestMethod()]
        public void EndSendGetDateTimeRequestTest()
        {
            throw new AssertInconclusiveException();
        }

        [TestMethod()]
        public void SendGetDateTimeRequestTest()
        {
            throw new AssertInconclusiveException();
        }
    }
}
