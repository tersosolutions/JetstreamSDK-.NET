using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CCE = TersoSolutions.Jetstream.SDK.Application.Messages.CommandCompletionEvent;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Messages
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CommandCompletionEventTests
    {
        [TestMethod]
        [TestCategory("Application Messages")]
        public void BuildCommandCompletionEventTest()
        {
            // DeviceExtensionListDeviceExtension
            CCE.JetstreamCommandCompletionEventDeviceExtensionListDeviceExtension delde =
                new CCE.JetstreamCommandCompletionEventDeviceExtensionListDeviceExtension
                {
                    Name = "Device",
                    Value = "FAKEUNIT001"
                };
            List<CCE.JetstreamCommandCompletionEventDeviceExtensionListDeviceExtension> deldeList =
                new List<CCE.JetstreamCommandCompletionEventDeviceExtensionListDeviceExtension> {delde};

            // DeviceExtensionList
            CCE.JetstreamCommandCompletionEventDeviceExtensionList del =
                new CCE.JetstreamCommandCompletionEventDeviceExtensionList {DeviceExtension = deldeList.ToArray()};

            // ExceptionList
            CCE.JetstreamCommandCompletionEventExceptionListException ele =
                new CCE.JetstreamCommandCompletionEventExceptionListException
                {
                    Message = "FAKEMESSAGE",
                    Name = "FAKEVALUE"
                };
            List<CCE.JetstreamCommandCompletionEventExceptionListException> elesList =
                new List<CCE.JetstreamCommandCompletionEventExceptionListException> {ele};
            CCE.JetstreamCommandCompletionEventExceptionList eleList = new CCE.JetstreamCommandCompletionEventExceptionList();
            eleList.Exception = elesList.ToArray();

            // OutputParameterListOutputParameter
            CCE.JetstreamCommandCompletionEventOutputParameterListOutputParameter oplop =
                new CCE.JetstreamCommandCompletionEventOutputParameterListOutputParameter
                {
                    Name = "EPC",
                    Value = "0909A62B1C07008430001022"
                };
            List<CCE.JetstreamCommandCompletionEventOutputParameterListOutputParameter> oplopList =
                new List<CCE.JetstreamCommandCompletionEventOutputParameterListOutputParameter> {oplop};

            // OutputParameterList
            CCE.JetstreamCommandCompletionEventOutputParameterList opl = new CCE.JetstreamCommandCompletionEventOutputParameterList();
            opl.OutputParameter = oplopList.ToArray();

            // CommandCompletionEvent
            CCE.JetstreamCommandCompletionEvent cce = new CCE.JetstreamCommandCompletionEvent
            {
                CommandId = System.Guid.NewGuid().ToString(),
                DeviceExtensionList = del,
                OutputParameterList = opl,
                ExceptionList = eleList
            };

            // Header
            CCE.JetstreamHeader header = new CCE.JetstreamHeader
            {
                EventId = Guid.NewGuid().ToString(),
                EventTime = DateTime.UtcNow.AddMinutes(-5),
                ReceivedTime = DateTime.UtcNow.AddMinutes(-2)
            };

            // Root node
            CCE.Jetstream root = new CCE.Jetstream
            {
                Header = header,
                CommandCompletionEvent = cce
            };

            // Asserts
            // Exercise the class getters
            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Header);
            Assert.IsNotNull(root.Header.EventId);
            Assert.IsNotNull(root.Header.EventTime);
            Assert.IsNotNull(root.Header.ReceivedTime);
            Assert.IsNull(root.Header.AnyAttr);
            Assert.IsNotNull(root.CommandCompletionEvent);
            Assert.IsNotNull(root.CommandCompletionEvent.CommandId);
            Assert.IsNull(root.CommandCompletionEvent.AnyAttr);
            Assert.IsNotNull(root.CommandCompletionEvent.DeviceExtensionList);
            Assert.IsNull(root.CommandCompletionEvent.DeviceExtensionList.AnyAttr);
            Assert.IsNotNull(root.CommandCompletionEvent.DeviceExtensionList.DeviceExtension);
            Assert.IsNotNull(root.CommandCompletionEvent.DeviceExtensionList.DeviceExtension.First().Value);
            Assert.IsNotNull(root.CommandCompletionEvent.DeviceExtensionList.DeviceExtension.First().Name);
            Assert.IsNull(root.CommandCompletionEvent.DeviceExtensionList.DeviceExtension.First().AnyAttr);
            Assert.IsNotNull(root.CommandCompletionEvent.OutputParameterList.OutputParameter);
            Assert.IsNotNull(root.CommandCompletionEvent.OutputParameterList.OutputParameter.First().Value);
            Assert.IsNotNull(root.CommandCompletionEvent.OutputParameterList.OutputParameter.First().Name);
            Assert.IsNull(root.CommandCompletionEvent.OutputParameterList.OutputParameter.First().AnyAttr);
            Assert.IsNull(root.CommandCompletionEvent.OutputParameterList.AnyAttr);
            Assert.IsNull(root.CommandCompletionEvent.ExceptionList.AnyAttr);
            Assert.IsNull(root.CommandCompletionEvent.ExceptionList.Exception.First().AnyAttr);
            Assert.IsNotNull(root.CommandCompletionEvent.ExceptionList.Exception.First().Message);
            Assert.IsNotNull(root.CommandCompletionEvent.ExceptionList.Exception.First().Name);
        }
    }
}
