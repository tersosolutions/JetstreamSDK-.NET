using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DRE = TersoSolutions.Jetstream.SDK.Application.Messages.DeviceRestoreEvent;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Messages
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DeviceRestoreEventTests
    {
        [TestMethod]
        [TestCategory("Application Messages")]
        public void BuildDeviceRestoreEventTest()
        {
            // JetstreamDeviceRestoreEvent
            DRE.JetstreamDeviceRestoreEvent deviceRestoreEvent = new DRE.JetstreamDeviceRestoreEvent();

            // JetstreamHeader
            DRE.JetstreamHeader header = new DRE.JetstreamHeader
            {
                EventId = Guid.NewGuid().ToString(),
                EventTime = DateTime.UtcNow.AddMinutes(-30),
                LogicalDeviceId = "MyLogicalDeviceId"
            };

            // Jetstream
            DRE.Jetstream root = new DRE.Jetstream
            {
                DeviceRestoreEvent = deviceRestoreEvent,
                Header = header
            };

            // Asserts
            // Exercises the gets in the class
            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Header);
            Assert.IsNotNull(root.Header.EventId);
            Assert.IsNotNull(root.Header.EventTime);
            Assert.IsNotNull(root.Header.LogicalDeviceId);
            Assert.IsNull(root.Header.AnyAttr);
            Assert.IsNotNull(root.DeviceRestoreEvent);
            Assert.IsNull(root.DeviceRestoreEvent.AnyAttr);
            Assert.IsNull(root.DeviceRestoreEvent.Any);
        }
    }
}
