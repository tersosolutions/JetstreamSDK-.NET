using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OE = TersoSolutions.Jetstream.SDK.Application.Messages.ObjectEvent;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Messages
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ObjectEventTests
    {
        [TestMethod]
        [TestCategory("Application Messages")]
        public void BuildObjectEventTest()
        {
            // JetstreamObjectEventDeviceExtensionListDeviceExtension
            OE.JetstreamObjectEventDeviceExtensionListDeviceExtension extension = new OE.JetstreamObjectEventDeviceExtensionListDeviceExtension();
            extension.Name = "MyObjectEventExtension";
            extension.Value = "MyObjectEventValue";
            
            // JetstreamObjectEventDeviceExtensionList
            OE.JetstreamObjectEventDeviceExtensionList extensionListList = new OE.JetstreamObjectEventDeviceExtensionList();
            List<OE.JetstreamObjectEventDeviceExtensionListDeviceExtension> extensionList = new List<OE.JetstreamObjectEventDeviceExtensionListDeviceExtension>();
            extensionList.Add(extension);
            extensionListList.DeviceExtension = extensionList.ToArray();

            // JetstreamObjectEventActionEPCListType
            OE.JetstreamObjectEventActionEPCListType listType = OE.JetstreamObjectEventActionEPCListType.OBSERVE;

            // JetstreamObjectEventActionEPCListEPC
            OE.JetstreamObjectEventActionEPCListEPC epc = new OE.JetstreamObjectEventActionEPCListEPC();
            epc.Value = "MyEpcValue";

            // JetstreamObjectEventActionEPCList
            OE.JetstreamObjectEventActionEPCList epcListList = new OE.JetstreamObjectEventActionEPCList();
            List<OE.JetstreamObjectEventActionEPCListEPC> epcList = new List<OE.JetstreamObjectEventActionEPCListEPC>();
            epcListList.EPC = epcList.ToArray();
            epcListList.Type = listType;

            // JetstreamObjectEvent
            OE.JetstreamObjectEvent objectEvent = new OE.JetstreamObjectEvent();
            objectEvent.ActionEPCList = epcListList;
            objectEvent.DeviceExtensionList = extensionListList;

            // JetstreamHeader
            OE.JetstreamHeader header = new OE.JetstreamHeader();
            header.EventId = Guid.NewGuid().ToString();
            header.EventTime = DateTime.UtcNow;
            header.LogicalDeviceId = "MyLogicalDeviceID";
            header.ReceivedTime = DateTime.UtcNow.AddHours(-1);

            // Jetstream
            OE.Jetstream root = new OE.Jetstream();
            root.Header = header;
            root.ObjectEvent = objectEvent;

            // Asserts
            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Header);
            Assert.IsNotNull(root.Header.EventId);
            Assert.IsNotNull(root.Header.EventTime);
            Assert.IsNull(root.Header.AnyAttr);
            Assert.IsNotNull(root.Header.LogicalDeviceId);
            Assert.IsNotNull(root.Header.ReceivedTime);
            Assert.IsNotNull(root.ObjectEvent);
            Assert.IsNotNull(root.ObjectEvent.ActionEPCList);
            Assert.IsNotNull(root.ObjectEvent.ActionEPCList.EPC);
            Assert.IsNotNull(root.ObjectEvent.ActionEPCList.Type);
            Assert.IsNull(root.ObjectEvent.ActionEPCList.AnyAttr);
            Assert.IsNotNull(root.ObjectEvent.DeviceExtensionList);
            Assert.IsNotNull(root.ObjectEvent.DeviceExtensionList.DeviceExtension);
            Assert.IsNull(root.ObjectEvent.DeviceExtensionList.AnyAttr);
            Assert.IsNull(root.ObjectEvent.AnyAttr);
            Assert.IsNull(root.ObjectEvent.Any);
            Assert.IsNull(root.EventId);
            Assert.IsNotNull(root.EventTime);            


        }
    }
}
