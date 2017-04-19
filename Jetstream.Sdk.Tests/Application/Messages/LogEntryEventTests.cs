using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LE = TersoSolutions.Jetstream.SDK.Application.Messages.LogEntryEvent;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Messages
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LogEntryEventTests
    {
        [TestMethod]
        [TestCategory("Application Messages")]
        public void BuildLogEntryEventTest()
        {
            // JetstreamLogEntryEventLogEntryListLogEntryParameterListParameter
            LE.JetstreamLogEntryEventLogEntryListLogEntryParameterListParameter parameter =
                new LE.JetstreamLogEntryEventLogEntryListLogEntryParameterListParameter
                {
                    Name = "MyParameterName",
                    Value = "MyParameterValue"
                };
            List<LE.JetstreamLogEntryEventLogEntryListLogEntryParameterListParameter> parameters =
                new List<LE.JetstreamLogEntryEventLogEntryListLogEntryParameterListParameter> {parameter};

            // JetstreamLogEntryEventLogEntryListLogEntryParameterList
            LE.JetstreamLogEntryEventLogEntryListLogEntryParameterList parameterList = new LE.JetstreamLogEntryEventLogEntryListLogEntryParameterList();
            parameterList.Parameter = parameters.ToArray();

            // JetstreamLogEntryEventLogEntryListLogEntryLevel
            LE.JetstreamLogEntryEventLogEntryListLogEntryLevel level = LE.JetstreamLogEntryEventLogEntryListLogEntryLevel.Information;

            // JetstreamLogEntryEventLogEntryListLogEntry
            LE.JetstreamLogEntryEventLogEntryListLogEntry logEntry = new LE.JetstreamLogEntryEventLogEntryListLogEntry
            {
                Level = level,
                LogTime = DateTime.UtcNow.AddHours(-1),
                Message = "My log entry message",
                ParameterList = parameterList,
                Type = "MyLogEntryType"
            };
            List<LE.JetstreamLogEntryEventLogEntryListLogEntry> logEntries =
                new List<LE.JetstreamLogEntryEventLogEntryListLogEntry> {logEntry};

            // JetstreamLogEntryEventLogEntryList
            LE.JetstreamLogEntryEventLogEntryList list = new LE.JetstreamLogEntryEventLogEntryList
            {
                LogEntry = logEntries.ToArray()
            };

            // JetstreamLogEntryEvent
            LE.JetstreamLogEntryEvent logEntryEvent = new LE.JetstreamLogEntryEvent {LogEntryList = list};

            // JetstreamHeader
            LE.JetstreamHeader header = new LE.JetstreamHeader
            {
                EventId = Guid.NewGuid().ToString(),
                EventTime = DateTime.UtcNow.AddHours(-1),
                LogicalDeviceId = "MyLogicalDeviceId",
                ReceivedTime = DateTime.UtcNow
            };


            // Jetstream
            LE.Jetstream root = new LE.Jetstream
            {
                Header = header,
                LogEntryEvent = logEntryEvent
            };

            // Asserts
            // Exercise the gets in the class
            Assert.IsNotNull(root);
            Assert.IsNull(root.EventId);
            Assert.IsNotNull(root.EventTime);
            Assert.IsNotNull(root.Header);
            Assert.IsNull(root.Header.AnyAttr);
            Assert.IsNotNull(root.Header.EventId);
            Assert.IsNotNull(root.Header.EventTime);
            Assert.IsNotNull(root.Header.LogicalDeviceId);
            Assert.IsNotNull(root.Header.ReceivedTime);
            Assert.IsNotNull(root.LogEntryEvent);
            Assert.IsNull(root.LogEntryEvent.Any);
            Assert.IsNull(root.LogEntryEvent.AnyAttr);
            Assert.IsNotNull(root.LogEntryEvent.LogEntryList);
            Assert.IsNull(root.LogEntryEvent.LogEntryList.AnyAttr);
            Assert.IsNotNull(root.LogEntryEvent.LogEntryList.LogEntry);
            Assert.IsNotNull(root.LogEntryEvent.LogEntryList.LogEntry.First().Level);
            Assert.IsNotNull(root.LogEntryEvent.LogEntryList.LogEntry.First().LogTime);
            Assert.IsNotNull(root.LogEntryEvent.LogEntryList.LogEntry.First().Message);
            Assert.IsNotNull(root.LogEntryEvent.LogEntryList.LogEntry.First().Type);
            Assert.IsNull(root.LogEntryEvent.LogEntryList.LogEntry.First().AnyAttr);
            Assert.IsNull(root.LogEntryEvent.LogEntryList.LogEntry.First().Any);
            Assert.IsNotNull(root.LogEntryEvent.LogEntryList.LogEntry.First().ParameterList.Parameter);
            Assert.IsNull(root.LogEntryEvent.LogEntryList.LogEntry.First().ParameterList.AnyAttr);
        }
    }
}
