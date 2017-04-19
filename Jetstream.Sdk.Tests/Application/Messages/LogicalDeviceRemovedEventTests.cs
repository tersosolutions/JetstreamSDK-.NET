using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LRE = TersoSolutions.Jetstream.SDK.Application.Messages.LogicalDeviceRemovedEvent;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Messages
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LogicalDeviceRemovedEventTests
    {
        [TestMethod]
        [TestCategory("Application Messages")]
        public void BuildLogicalDeviceRemovedEventTest()
        {
            // LogicalDeviceRemovedEvent
            LRE.JetstreamLogicalDeviceRemovedEvent removedEvent = new LRE.JetstreamLogicalDeviceRemovedEvent();
            

            // JetstreamHeader
            LRE.JetstreamHeader header = new LRE.JetstreamHeader
            {
                EventId = Guid.NewGuid().ToString(),
                LogicalDeviceId = "MyLogicalDeviceId",
                EventTime = DateTime.UtcNow,
            };

            // Jetstream
            LRE.Jetstream root = new LRE.Jetstream
            {
                Header = header,
                LogicalDeviceRemovedEvent = removedEvent
            };

            // Asserts
            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Header);
            Assert.IsNotNull(root.LogicalDeviceRemovedEvent);
            Assert.IsNull(root.LogicalDeviceRemovedEvent.AnyAttr);
            Assert.IsNull(root.LogicalDeviceRemovedEvent.Any);
            Assert.IsNull(root.EventId);
            Assert.IsNotNull(root.EventTime);
           
        }
    }
}
