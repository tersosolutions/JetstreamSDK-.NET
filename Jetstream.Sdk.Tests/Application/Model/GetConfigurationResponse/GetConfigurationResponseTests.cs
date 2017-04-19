using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GR = TersoSolutions.Jetstream.SDK.Application.Model.Deserialized.GetConfigurationResponse;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Model.GetConfigurationResponse
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetConfigurationResponseTests
    {
        #region Privates

        GR.JetstreamGetConfigurationResponseLogicalDeviceRegion _deviceRegion = GR.JetstreamGetConfigurationResponseLogicalDeviceRegion.US;
        GR.JetstreamGetConfigurationResponseLogicalDeviceHealth _deviceHealth = GR.JetstreamGetConfigurationResponseLogicalDeviceHealth.Active;
        GR.JetstreamGetConfigurationResponseLogicalDevice _logicalDevice = new GR.JetstreamGetConfigurationResponseLogicalDevice();
        GR.JetstreamGetConfigurationResponse _response = new GR.JetstreamGetConfigurationResponse();
        GR.JetstreamHeader _header = new GR.JetstreamHeader();
        GR.Jetstream _root = new GR.Jetstream();
        private string _tempfilePath = ConfigurationManager.AppSettings["TempFilesPath"];

        #endregion

        #region Logical Device

        [TestMethod]
        [TestCategory("Application Model")]
        public void BuildLogicalDeviceTest()
        {
            // Act, set properties
            _logicalDevice.DeviceDefinitionId = Guid.NewGuid().ToString();
            _logicalDevice.DeviceSerialNumber = "MyLogicalDeviceSerialNumber";
            _logicalDevice.Health = _deviceHealth;
            _logicalDevice.LogicalDeviceId = "MyLogicalDeviceId";
            _logicalDevice.PolicyId = Guid.NewGuid().ToString();
            _logicalDevice.Region = _deviceRegion;

            // Asserts
            Assert.IsNotNull(_logicalDevice.DeviceDefinitionId);
            Assert.IsNotNull(_logicalDevice.DeviceSerialNumber);
            Assert.IsNotNull(_logicalDevice.Health);
            Assert.IsNotNull(_logicalDevice.LogicalDeviceId);
            Assert.IsNotNull(_logicalDevice.PolicyId);
            Assert.IsNotNull(_logicalDevice.Region);
            Assert.IsTrue(!_logicalDevice.AnyAttr.Any());

        }

        [TestMethod]
        [TestCategory("Application Model")]
        public void SaveToFileLogicalDeviceTest()
        {
            // tough to test a void
            _logicalDevice.SaveToFile(@String.Format("{0}GetConfigurationResponseLogicalDevice.txt", _tempfilePath));
        }

        [TestMethod]
        [TestCategory("Application Model")]
        public void LoadFromFileLogicalDeviceTest()
        {
            // assemble
            GR.JetstreamGetConfigurationResponseLogicalDevice getConfigurationResponseLogicalDevice = new GR.JetstreamGetConfigurationResponseLogicalDevice();

            // act
            bool success = GR.JetstreamGetConfigurationResponseLogicalDevice.LoadFromFile(
                @String.Format("{0}GetConfigurationResponseLogicalDevice.txt", _tempfilePath), out getConfigurationResponseLogicalDevice);

            // assert
            Assert.IsTrue(success);

        }

        #endregion

        #region GetConfigurationResponse

        [TestMethod]
        [TestCategory("Application Model")]
        public void BuildGetConfigurationResponseTest()
        {
            // Act, set properties
            _response.LogicalDeviceList = new List<GR.JetstreamGetConfigurationResponseLogicalDevice>();
            _response.LogicalDeviceList.Add(_logicalDevice);

            // Asserts
            Assert.IsNotNull(_response.LogicalDeviceList);
            Assert.IsTrue(_response.LogicalDeviceList.Any());
            Assert.IsTrue(!_response.Any.Any());
            Assert.IsTrue(!_response.AnyAttr.Any());
        }

        [TestMethod]
        [TestCategory("Application Model")]
        public void SaveToFileGetConfigurationResponseTest()
        {
            // tough to test a void
            _response.SaveToFile(@String.Format("{0}GetConfigurationResponse.txt", _tempfilePath));            
        }

        [TestMethod]
        [TestCategory("Application Model")]
        public void LoadFromFileGetConfigurationResponse()
        {
            // assemble
            GR.JetstreamGetConfigurationResponse getConfigurationResponse = new GR.JetstreamGetConfigurationResponse();

            // act
            bool success = GR.JetstreamGetConfigurationResponse.LoadFromFile(
                @String.Format("{0}GetConfigurationResponse.txt", _tempfilePath), out getConfigurationResponse);

            // assert
            Assert.IsTrue(success);
        }

        #endregion
    }
}

