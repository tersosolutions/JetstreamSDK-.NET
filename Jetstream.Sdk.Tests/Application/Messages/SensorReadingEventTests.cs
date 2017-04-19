using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SR = TersoSolutions.Jetstream.SDK.Application.Messages.SensorReadingEvent;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Messages
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SensorReadingEventTests
    {
        [TestMethod]
        [TestCategory("Application Messages")]
        public void BuildSensorReadingEventTest()
        {
            // JetstreamSensorReadingEventReadingListReading
            SR.JetstreamSensorReadingEventReadingListReading reading =
                new SR.JetstreamSensorReadingEventReadingListReading
                {
                    Name = "TempartureA",
                    Value = "23",
                    ReadingTime = DateTime.UtcNow.AddHours(-1)
                };

            // JetstreamSensorReadingEventReadingList
            SR.JetstreamSensorReadingEventReadingList readingList = new SR.JetstreamSensorReadingEventReadingList
            {
                Reading = new List<SR.JetstreamSensorReadingEventReadingListReading>()
                {
                    reading
                }.ToArray()
            };

            // JetstreamSensorReadingEvent
            SR.JetstreamSensorReadingEvent sensorReadingEvent = new SR.JetstreamSensorReadingEvent
            {
                ReadingList = readingList
            };

            // JetstreamHeader
            SR.JetstreamHeader header = new SR.JetstreamHeader
            {
                EventId = Guid.NewGuid().ToString(),
                EventTime = DateTime.UtcNow.AddHours(-2),
                LogicalDeviceId = "MyLogicalDeviceId",
                ReceivedTime = DateTime.UtcNow.AddHours(-1)
            };

            // Jetstream
            SR.Jetstream root = new SR.Jetstream
            {
                Header = header,
                SensorReadingEvent = sensorReadingEvent
            };

            // Asserts
            Assert.IsNotNull(root);
            Assert.IsNull(root.EventId);
            Assert.IsNotNull(root.EventTime);
            Assert.IsNull(root.EventType);
            Assert.IsNotNull(root.SensorReadingEvent);
            Assert.IsNotNull(root.SensorReadingEvent.ReadingList);
            Assert.IsNull(root.SensorReadingEvent.AnyAttr);
            Assert.IsNull(root.SensorReadingEvent.AnyAttr);
            Assert.IsNotNull(root.SensorReadingEvent.ReadingList.Reading.First().Value);
            Assert.IsNotNull(root.SensorReadingEvent.ReadingList.Reading.First().Name);
            Assert.IsNotNull(root.SensorReadingEvent.ReadingList.Reading.First().ReadingTime);
            Assert.IsNull(root.SensorReadingEvent.ReadingList.Reading.First().AnyAttr);
            Assert.IsNotNull(root.Header.EventId);
            Assert.IsNotNull(root.Header.EventTime);
            Assert.IsNotNull(root.Header.LogicalDeviceId);
            Assert.IsNotNull(root.Header.ReceivedTime);
            Assert.IsNull(root.Header.AnyAttr);

        }
    }
}
