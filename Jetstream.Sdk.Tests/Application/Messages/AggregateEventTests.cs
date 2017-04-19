using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AE = TersoSolutions.Jetstream.SDK.Application.Messages.AggregateEvent;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Messages
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AggregateEventTests
    {
        [TestMethod]
        [TestCategory("Application Messages")]
        public void BuildAggregateEventTest()
        {
            // JetstreamAggregateEventActionEPCListsActionEPCList Adds
            // makes the epcs for the addEpcs
            var addEpcs = new List<string> { "MyTag001", "MyTag002" };
            // Creates the actionEPCList and adds the EPCs to it
            var addActionEpcList = new AE.JetstreamAggregateEventActionEPCListsActionEPCList
            {
                Type = AE.JetstreamAggregateEventActionEPCListsActionEPCListType.ADD,
                EPC = addEpcs.Select(addEpc => new AE.JetstreamAggregateEventActionEPCListsActionEPCListEPC {Value = addEpc}).ToArray()
            };

            // JetstreamAggregateEventActionEPCListsActionEPCList Removes
            // makes the epcs for the removeEpcs
            var removeEpcs = new List<string> { "MyTag003", "MyTag004" };
            // Creates the actionEPCList and adds the EPCs to it

            var removeActionEpcList = new AE.JetstreamAggregateEventActionEPCListsActionEPCList
            {
                Type = AE.JetstreamAggregateEventActionEPCListsActionEPCListType.REMOVE,
                EPC =
                    removeEpcs.Select(
                        removeEpc => new AE.JetstreamAggregateEventActionEPCListsActionEPCListEPC {Value = removeEpc})
                        .ToArray()
            };

            // JetstreamAggregateEventDeviceExtensionListDeviceExtension
            var deviceExtension = new AE.JetstreamAggregateEventDeviceExtensionListDeviceExtension
            {
                Value = "MyPass001",
                Name = "PassRfid"
            };
            var deviceExtensions = new List<AE.JetstreamAggregateEventDeviceExtensionListDeviceExtension>
            {
                deviceExtension
            };

            var deviceExtensionList = new AE.JetstreamAggregateEventDeviceExtensionList
            {
                DeviceExtension = deviceExtensions.ToArray()
            };

            // JetstreamAggregateEventActionEPCListsActionEPCList
            var actionEpcList = new List<AE.JetstreamAggregateEventActionEPCListsActionEPCList>
            {
                addActionEpcList,
                removeActionEpcList
            };

            // JetstreamAggregateEventActionEPCLists
            var lists = new AE.JetstreamAggregateEventActionEPCLists { ActionEPCList = actionEpcList.ToArray() };
            var aggregateEvent = new AE.JetstreamAggregateEvent
            {
                ActionEPCLists = lists,
                DeviceExtensionList = deviceExtensionList
            };

    
            // JetstreamHeader
            var header = new AE.JetstreamHeader
            {
                EventId = Guid.NewGuid().ToString(),
                EventTime = DateTime.UtcNow.AddHours(-1),
                LogicalDeviceId = "MyLogicalDeviceId",
                ReceivedTime = DateTime.UtcNow
            };

            // Jetstream
            var aggEvent = new AE.Jetstream
            {
                AggregateEvent = aggregateEvent,
                Header = header
            };

            // Asserts
            // Exercise the GETS for the object
            Assert.IsNotNull(aggEvent.Header);
            Assert.IsNull(aggEvent.Header.AnyAttr);
            Assert.IsNotNull(aggEvent.Header.EventId);
            Assert.IsNotNull(aggEvent.Header.EventTime);
            Assert.IsNotNull(aggEvent.Header.LogicalDeviceId);
            Assert.IsNotNull(aggEvent.Header.ReceivedTime);
            Assert.IsNotNull(aggEvent.AggregateEvent);
            Assert.IsNull(aggEvent.AggregateEvent.AnyAttr);
            Assert.IsNotNull(aggEvent.AggregateEvent.ActionEPCLists.ActionEPCList);
            Assert.IsNotNull(aggEvent.AggregateEvent.ActionEPCLists.ActionEPCList.First().EPC);
            Assert.IsNull(aggEvent.AggregateEvent.ActionEPCLists.ActionEPCList.First().EPC.First().AnyAttr);
            Assert.IsNotNull(aggEvent.AggregateEvent.ActionEPCLists.ActionEPCList.First().EPC.First().Value);
            Assert.IsNull(aggEvent.AggregateEvent.ActionEPCLists.ActionEPCList.First().AnyAttr);
            Assert.IsNotNull(aggEvent.AggregateEvent.ActionEPCLists.ActionEPCList.First().Type);
            Assert.IsNull(aggEvent.AggregateEvent.ActionEPCLists.AnyAttr);
            Assert.IsNull(aggEvent.AggregateEvent.Any);
            Assert.IsNotNull(aggEvent.AggregateEvent.DeviceExtensionList);
            Assert.IsNull(aggEvent.AggregateEvent.DeviceExtensionList.AnyAttr);
            Assert.IsNotNull(aggEvent.AggregateEvent.DeviceExtensionList.DeviceExtension);
            Assert.IsNull(aggEvent.AggregateEvent.DeviceExtensionList.DeviceExtension.First().AnyAttr);
            Assert.IsNotNull(aggEvent.AggregateEvent.DeviceExtensionList.DeviceExtension.First().Value);
            Assert.IsNotNull(aggEvent.AggregateEvent.DeviceExtensionList.DeviceExtension.First().Name);
        }
    }
}
