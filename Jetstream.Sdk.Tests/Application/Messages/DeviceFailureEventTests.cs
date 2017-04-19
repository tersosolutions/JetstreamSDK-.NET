using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DFE = TersoSolutions.Jetstream.SDK.Application.Messages.DeviceFailureEvent;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Messages
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DeviceFailureEventTests
    {
        [TestMethod]
        [TestCategory("Application Messages")]
        public void BuildDeviceFailureEventTest()
        {
            // DeviceFailureEvent
            DFE.JetstreamDeviceFailureEvent deviceFailureEvent = new DFE.JetstreamDeviceFailureEvent();
            
            // JetstreamHeader
            DFE.JetstreamHeader header = new DFE.JetstreamHeader
            {
                EventId = Guid.NewGuid().ToString(),
                EventTime = DateTime.UtcNow.AddMinutes(-30),
                LogicalDeviceId = "MyLogicalDeviceId"
            };

            // Jetstream
            DFE.Jetstream root = new DFE.Jetstream
            {
                Header = header,
                DeviceFailureEvent = deviceFailureEvent
            };

            // Asserts
            // Exercises the gets in the classes
            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Header);
            Assert.IsNotNull(root.Header.EventId);
            Assert.IsNotNull(root.Header.EventTime);
            Assert.IsNotNull(root.Header.LogicalDeviceId);
            Assert.IsNull(root.Header.AnyAttr);
            Assert.IsNotNull(root.DeviceFailureEvent);
            Assert.IsNull(root.DeviceFailureEvent.Any);
            Assert.IsNull(root.DeviceFailureEvent.AnyAttr);

        }
    }
}
