/*
    Copyright 2022 Terso Solutions, Inc.

  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at

      http://www.apache.org/licenses/LICENSE-2.0

  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.
*/

using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace TersoSolutions.Jetstream.Sdk.Tests
{
    /// <summary>
    /// Test class to verify that the factory create methods work
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class JetstreamClientFactoryTests
    {
        /// <summary>
        /// Verify that the create method using a url string works
        /// </summary>
        [TestMethod]
        public void VerifyCreateString()
        {
            // Assemble
            var factory = new JetstreamClientFactory();

            // Act
            var result = factory.Create("key", "http://url.com");

            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Verify that the create method using a URI works
        /// </summary>
        [TestMethod]
        public void VerifyCreateUri()
        {
            // Assemble
            var factory = new JetstreamClientFactory();
            var uri = new Uri("http://url.com");

            // Act
            var result = factory.Create("key", uri);

            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Verify that the create method using JetstreamClientOptions works
        /// </summary>
        [TestMethod]
        public void VerifyCreateOptions()
        {
            // Assemble
            var factory = new JetstreamClientFactory();
            var options = new JetstreamClientOptions
            {
                AccessKey = "key",
                JetstreamUrl = new Uri("http://url.com")
            };

            Mock<IOptionsMonitor<JetstreamClientOptions>> MockOption = new Mock<IOptionsMonitor<JetstreamClientOptions>>();
            MockOption.Setup(x => x.CurrentValue).Returns(options);

            // Act
            var result = factory.Create(MockOption.Object);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
