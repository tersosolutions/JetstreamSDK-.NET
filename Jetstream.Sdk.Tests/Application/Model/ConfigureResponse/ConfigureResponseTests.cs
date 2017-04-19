using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRE = TersoSolutions.Jetstream.SDK.Application.Model.Deserialized.ConfigureResponse;

namespace TersoSolutions.Jetstream.SDK.Tests.Application.Model.ConfigureResponse
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ConfigureResponseTests
    {
        #region privates

        CRE.Jetstream _configureResponse = new CRE.Jetstream();
        private string _tempfilePath = ConfigurationManager.AppSettings["TempFilesPath"];

        #endregion

        [TestMethod]
        [TestCategory("Application Model")]
        public void BuildConfigureResponseTest()
        {
            // JetstreamConfigureResponse
            CRE.JetstreamConfigureResponse response = new CRE.JetstreamConfigureResponse();
            response.Id = Guid.NewGuid().ToString();

            // Jetstream Header
            CRE.JetstreamHeader header = new CRE.JetstreamHeader();

            // Jetstream
            _configureResponse = new CRE.Jetstream();
            _configureResponse.ConfigureResponse = response;
            _configureResponse.Header = header;

            // Act
            response.SaveToFile(String.Format("{0}responseTest.txt", _tempfilePath));
            CRE.JetstreamConfigureResponse loadedConfigureResponse = new CRE.JetstreamConfigureResponse();
            bool responseLoad = CRE.JetstreamConfigureResponse.LoadFromFile(@String.Format("{0}responseTest.txt", _tempfilePath), out loadedConfigureResponse);

            header.SaveToFile(@String.Format("{0}responseHeaderTest.txt", _tempfilePath));
            CRE.JetstreamHeader loadedHeader = new CRE.JetstreamHeader();
            bool responseHeaderLoaded = CRE.JetstreamHeader.LoadFromFile(@String.Format("{0}responseHeaderTest.txt", _tempfilePath),
                out loadedHeader);

            // Asserts
            Assert.IsTrue(responseLoad);
            Assert.IsTrue(responseHeaderLoaded);
            Assert.IsNotNull(_configureResponse);
            Assert.IsNotNull(_configureResponse.ConfigureResponse);
            Assert.IsTrue(!_configureResponse.ConfigureResponse.AnyAttr.Any());
            Assert.IsTrue(!_configureResponse.ConfigureResponse.Any.Any());
            Assert.IsNotNull(_configureResponse.ConfigureResponse.Id);
            Assert.IsNotNull(_configureResponse.Header);
            Assert.IsTrue(!_configureResponse.Header.AnyAttr.Any());
        }

        [TestMethod]
        [TestCategory("Application Model")]
        public void SaveToFileConfigureResponse()
        {

            // Pretty hard to test a void...
            _configureResponse.SaveToFile(String.Format("{0}ConfigureResponseTest.txt", _tempfilePath));
        }

        [TestMethod]
        [TestCategory("Application Model")]
        public void LoadToFileConfigureResponse()
        {
            // assemble the file and compare against the local
            CRE.Jetstream loadedConfigureResponse = new CRE.Jetstream();
            bool success = CRE.Jetstream.LoadFromFile(@String.Format("{0}ConfigureResponseTest.txt", _tempfilePath), out loadedConfigureResponse);

            // Asserts
            Assert.IsTrue(success);
            // Assert.AreEqual(loadedConfigureResponse, _configureResponse);
            Assert.AreEqual(_configureResponse.ConfigureResponse.Id, loadedConfigureResponse.ConfigureResponse.Id);
        }
    }
}
