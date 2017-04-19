using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CR = TersoSolutions.Jetstream.SDK.Application.Model.Deserialized.CommandResponse;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Model.CommandResponse
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CommandResponseTests
    {
        #region Privates

        private string _tempfilePath = ConfigurationManager.AppSettings["TempFilesPath"];

        #endregion

        [TestMethod]
        [TestCategory("Application Model")]
        public void BuildCommandResponseTest()
        {
            // JetstreamCommandResponseOutputParameterListOutputParameter
            CR.JetstreamCommandResponseOutputParameterListOutputParameter outputParameter = new CR.JetstreamCommandResponseOutputParameterListOutputParameter();
            outputParameter.Name = "MyOutputParameter";
            outputParameter.Value = "MyValue";

            // JetstreamCommandResponseOutputParameterList
            CR.JetstreamCommandResponseOutputParameterList outputParameterList = new CR.
                JetstreamCommandResponseOutputParameterList
            {
                OutputParameter = new List<CR.JetstreamCommandResponseOutputParameterListOutputParameter>()
                {
                    outputParameter
                }.ToArray()
            };

            // JetstreamCommandResponseExceptionListException
            CR.JetstreamCommandResponseExceptionListException exception = new CR.JetstreamCommandResponseExceptionListException();
            exception.Message = "MyExceptionMessage";
            exception.Name = "MyExceptionName";

            // JetstreamCommandResponseExceptionList
            CR.JetstreamCommandResponseExceptionList exceptionList = new CR.JetstreamCommandResponseExceptionList();
            List<CR.JetstreamCommandResponseExceptionListException> exceptions = new List<CR.JetstreamCommandResponseExceptionListException>();
            exceptions.Add(exception);
            exceptionList.Exception = exceptions.ToArray();

            // JetstreamCommandResponseDeviceExtensionListDeviceExtension
            CR.JetstreamCommandResponseDeviceExtensionListDeviceExtension deviceExtension = new CR.JetstreamCommandResponseDeviceExtensionListDeviceExtension();
            deviceExtension.Name = "MyDeviceExtensionName";
            deviceExtension.Value = "MyDeviceExtensionValue";

            // JetstreamCommandResponseDeviceExtensionList
            CR.JetstreamCommandResponseDeviceExtensionList deviceExtensionList = new CR.JetstreamCommandResponseDeviceExtensionList();
            List<CR.JetstreamCommandResponseDeviceExtensionListDeviceExtension> deviceExtensions = new List<CR.JetstreamCommandResponseDeviceExtensionListDeviceExtension>();
            deviceExtensions.Add(deviceExtension);
            deviceExtensionList.DeviceExtension = deviceExtensions.ToArray();

            // JetstreamCommandResponse
            CR.JetstreamCommandResponse response = new CR.JetstreamCommandResponse();
            response.CommandId = Guid.NewGuid().ToString();
            response.DeviceExtensionList = deviceExtensionList;
            response.ExceptionList = exceptionList;
            response.OutputParameterList = outputParameterList;

            // JetstreamHeader
            CR.JetstreamHeader header = new CR.JetstreamHeader();

            // Jetstream
            CR.Jetstream root = new CR.Jetstream();
            root.CommandResponse = response;
            root.Header = header;

            // Test Save and Load
            root.SaveToFile(@String.Format("{0}commandResponseTest.txt", _tempfilePath));
            CR.Jetstream commandResponseLoad = new CR.Jetstream();
            CR.Jetstream.LoadFromFile(@String.Format("{0}commandResponseTest.txt", _tempfilePath));

            // Asserts
            Assert.IsNotNull(root);
            Assert.IsNotNull(root.CommandResponse);
            Assert.IsNotNull(root.CommandResponse.CommandId);
            Assert.IsNotNull(root.CommandResponse.DeviceExtensionList.DeviceExtension.First().Value);
            Assert.IsNotNull(root.CommandResponse.DeviceExtensionList.DeviceExtension.First().Name);
            Assert.IsNull(root.CommandResponse.DeviceExtensionList.DeviceExtension.First().AnyAttr);
            Assert.IsNotNull(root.CommandResponse.ExceptionList.Exception.First().Message);
            Assert.IsNotNull(root.CommandResponse.ExceptionList.Exception.First().Name);
            Assert.IsNull(root.CommandResponse.ExceptionList.Exception.First().AnyAttr);
            Assert.IsNotNull(root.CommandResponse.OutputParameterList.OutputParameter.First().Value);
            Assert.IsNotNull(root.CommandResponse.OutputParameterList.OutputParameter.First().Name);
            Assert.IsNull(root.CommandResponse.OutputParameterList.OutputParameter.First().AnyAttr);
            Assert.IsNull(root.CommandResponse.AnyAttr);
            Assert.IsNull(root.CommandResponse.Any);
            Assert.IsNull(root.Header.AnyAttr);
        }
    }
}
