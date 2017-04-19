using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HE = TersoSolutions.Jetstream.SDK.Application.Messages.HeartbeatEvent;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Messages
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class HeartbeatEventTests
    {
        [TestMethod]
        [TestCategory("Application Messages")]
        public void BuildHeartbeatEventTest()
        {
            // JetstreamHeartbeatEvent
            HE.JetstreamHeartbeatEvent heartbeatEvent = new HE.JetstreamHeartbeatEvent();

            // JetstreamHeader
            HE.JetstreamHeader header = new HE.JetstreamHeader
            {
                EventId = Guid.NewGuid().ToString(),
                EventTime = DateTime.UtcNow.AddMinutes(-30),
                LogicalDeviceId = "MyLogicalDeviceId",
                ReceivedTime = DateTime.UtcNow
            };

            // Jetstream
            HE.Jetstream root = new HE.Jetstream
            {
                Header = header,
                HeartbeatEvent = heartbeatEvent
            };

            // Asserts
            // Exercises gets in class
            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Header);
            Assert.IsNotNull(root.Header.EventId);
            Assert.IsNotNull(root.Header.EventTime);
            Assert.IsNotNull(root.Header.LogicalDeviceId);
            Assert.IsNotNull(root.Header.ReceivedTime);
            Assert.IsNull(root.Header.AnyAttr);
            Assert.IsNotNull(root.HeartbeatEvent);
            Assert.IsNull(root.HeartbeatEvent.Any);
            Assert.IsNull(root.HeartbeatEvent.AnyAttr);
        }
    }
}
