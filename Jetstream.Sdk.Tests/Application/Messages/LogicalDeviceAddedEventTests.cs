using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LE = TersoSolutions.Jetstream.SDK.Application.Messages.LogicalDeviceAddedEvent;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Messages
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LogicalDeviceAddedEventTests
    {
        [TestMethod]
        [TestCategory("Application Messages")]
        public void BuildLogicalDeviceAddedEventTest()
        {
            // JetstreamLogicalDeviceAddedEventRegion
            LE.JetstreamLogicalDeviceAddedEventRegion eventRegion = LE.JetstreamLogicalDeviceAddedEventRegion.AP;

            // JetstreamLogicalDeviceAddedEvent
            LE.JetstreamLogicalDeviceAddedEvent leEvent = new LE.JetstreamLogicalDeviceAddedEvent
            {
                DeviceDefinitionId = Guid.NewGuid().ToString(),
                DeviceSerialNumber = "MyDeviceSerialNumber",
                LogicalDeviceId = "MyLogicalDeviceId",
                Region = eventRegion
            };

            // JetstreamHeader
            LE.JetstreamHeader header = new LE.JetstreamHeader
            {
                EventId = Guid.NewGuid().ToString(),
                EventTime = DateTime.UtcNow
            };

            // Jetstream
            LE.Jetstream root = new LE.Jetstream
            {
                Header = header,
                LogicalDeviceAddedEvent = leEvent
            };

            // Asserts
            // Exercise the gets in the classes
            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Header);
            Assert.IsNull(root.Header.AnyAttr);
            Assert.IsNotNull(root.Header.EventId);
            Assert.IsNotNull(root.Header.EventTime);
            Assert.IsNotNull(root.LogicalDeviceAddedEvent);
            Assert.IsNotNull(root.LogicalDeviceAddedEvent.DeviceDefinitionId);
            Assert.IsNotNull(root.LogicalDeviceAddedEvent.DeviceSerialNumber);
            Assert.IsNotNull(root.LogicalDeviceAddedEvent.LogicalDeviceId);
            Assert.IsNotNull(root.LogicalDeviceAddedEvent.Region);
            Assert.IsNull(root.LogicalDeviceAddedEvent.AnyAttr);
            Assert.IsNull(root.LogicalDeviceAddedEvent.Any);
        }
    }
}
