using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CQE = TersoSolutions.Jetstream.SDK.Application.Messages.CommandQueuedEvent;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Messages
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CommandQueuedEventTests
    {
        [TestMethod]
        [TestCategory("Application Messages")]
        public void BuildCommandQueuedEventTest()
        {
            // CommandQueuedEventParameter
            CQE.JetstreamCommandQueuedEventParameter parameter = new CQE.JetstreamCommandQueuedEventParameter
            {
                Name = "Parameter0",
                Value = "aggregateeventscancount"
            };
            List<CQE.JetstreamCommandQueuedEventParameter> parameters =
                new List<CQE.JetstreamCommandQueuedEventParameter> {parameter};

            // CommandQueuedEvent
            CQE.JetstreamCommandQueuedEvent cqe = new CQE.JetstreamCommandQueuedEvent
            {
                CommandId = Guid.NewGuid().ToString(),
                CommandName = "GetConfigValuesCommand",
                UserName = "MyUserName",
                ParameterList = parameters.ToArray()
            };

            // Header
            CQE.JetstreamHeader header = new CQE.JetstreamHeader();
            header.EventId = Guid.NewGuid().ToString();
            header.EventTime = DateTime.UtcNow.AddMinutes(-30);
            header.LogicalDeviceId = "MyLogicalDeviceId";

            // Jetstream
            CQE.Jetstream root = new CQE.Jetstream();
            root.Header = header;
            root.CommandQueuedEvent = cqe;

            // Asserts
            // Exercise the gets in the classes
            Assert.IsNotNull(root.Header.EventId);
            Assert.IsNotNull(root.Header.EventTime);
            Assert.IsNotNull(root.Header.LogicalDeviceId);
            Assert.IsNull(root.Header.AnyAttr);
            Assert.IsNotNull(root.CommandQueuedEvent.CommandId);
            Assert.IsNotNull(root.CommandQueuedEvent.CommandName);
            Assert.IsNotNull(root.CommandQueuedEvent.UserName);
            Assert.IsNull(root.CommandQueuedEvent.AnyAttr);
            Assert.IsNull(root.CommandQueuedEvent.Any);
            Assert.IsNotNull(root.CommandQueuedEvent.ParameterList.First().Value);
            Assert.IsNotNull(root.CommandQueuedEvent.ParameterList.First().Name);

        }
    }
}
