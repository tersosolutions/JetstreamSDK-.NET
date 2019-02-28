/*
    Copyright 2019 Terso Solutions, Inc.

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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TersoSolutions.Jetstream.SDK.Objects;
using TersoSolutions.Jetstream.SDK.Objects.Events;

namespace TersoSolutions.Jetstream.SDK.Tests
{
    /// <summary>
    /// Method of mocking WebRequest taken from
    /// http://blog.salamandersoft.co.uk/index.php/2009/10/how-to-mock-httpwebrequest-when-unit-testing/
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class JetstreamClientTests
    {
        private readonly string _accessKey = Guid.NewGuid().ToString();

        #region Constructor Tests

        [TestMethod]
        [SuppressMessage("ReSharper", "UncatchableException")]
        public void ServiceClientTests_ConstructorNullUrl()
        {
            try
            {
                var unused = new JetstreamClient(_accessKey, "");
                Assert.Fail();
            }
            catch (ArgumentNullException e)
            {
                Assert.IsTrue(e.Message.Contains("jetstreamApiUrl"));
            }
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "UncatchableException")]
        public void ServiceClientTests_ConstructorNullKey()
        {
            try
            {
                var unused = new JetstreamClient("", "test://MyUrl");
                Assert.Fail();
            }
            catch (ArgumentNullException e)
            {
                Assert.IsTrue(e.Message.Contains("accessKey"));
            }
        }

        #endregion

        #region Event Tests

        [TestMethod]
        public void ServiceClientTests_GetEventsWithoutSearchAndSortHappyPath()
        {
            var heartbeatEvent = new HeartbeatEventDto
            {
                Device = "TestDevice",
                EventId = Guid.NewGuid().ToString(),
                EventTime = DateTime.Now,
                ReceivedTime = DateTime.Now,
                Type = "HeartbeatEvent"
            };
            var eventsDto = new EventsDto
            {
                Events = new List<EventDto> { heartbeatEvent },
                BatchId = Guid.NewGuid().ToString(),
                Count = 1
            };

            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(eventsDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetEvents();

            Assert.IsNotNull(result);
            Assert.AreEqual(eventsDto.Count, result.Count);
            Assert.AreEqual(eventsDto.BatchId, result.BatchId);
            var resultEvent = (HeartbeatEventDto)result.Events.First();
            Assert.AreEqual(heartbeatEvent.EventTime, resultEvent.EventTime);
            Assert.AreEqual(heartbeatEvent.EventId, resultEvent.EventId);
            Assert.AreEqual(heartbeatEvent.Type, resultEvent.Type);
            Assert.AreEqual(heartbeatEvent.Device, resultEvent.Device);
            Assert.AreEqual(heartbeatEvent.ReceivedTime, resultEvent.ReceivedTime);
        }

        [TestMethod]
        public void ServiceClientTests_GetEventsManyWithoutSearchAndSortHappyPath()
        {
            var listOfEvents = new List<EventDto>();

            for (int i = 0; i < 512; i++)
            {
                listOfEvents.Add(new HeartbeatEventDto
                {
                    Device = "TestDevice" + i,
                    EventId = Guid.NewGuid().ToString(),
                    EventTime = DateTime.Now,
                    ReceivedTime = DateTime.Now,
                    Type = "HeartbeatEvent"
                });
            }
            var eventsDto = new EventsDto
            {
                Events = listOfEvents,
                BatchId = Guid.NewGuid().ToString(),
                Count = 1
            };

            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(eventsDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetEvents();

            Assert.IsNotNull(result);
            Assert.AreEqual(eventsDto.Count, result.Count);
            Assert.AreEqual(eventsDto.BatchId, result.BatchId);
        }

        #endregion

        #region Device Definition Tests

        [TestMethod]
        public void ServiceClientTests_GetDeviceDefinitionsWithoutSearchAndSortHappyPath()
        {
            var deviceDefinitionDto = new DeviceDefinitionsDto
            {
                Name = "HelloWorld",
                SetConfigValuesCommand = true,
                GetConfigValuesCommand = true,
                GetEpcListCommand = true,
                ResetCommand = true,
                UpdateFirmwareCommand = true,
                FirmwareVersion = "4.5",
                ConfigParameters = new Dictionary<string, string>(),
                DeviceSpecificCommandNames = new List<string>(),
                SensorReadingMeasures = new Dictionary<string, string>()
            };
            var deviceList = new List<DeviceDefinitionsDto> { deviceDefinitionDto };

            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(deviceList));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetDeviceDefinitions();

            Assert.IsNotNull(result);
            Assert.AreEqual(deviceDefinitionDto.Name, result.First().Name);
            Assert.AreEqual(deviceDefinitionDto.SetConfigValuesCommand, result.First().SetConfigValuesCommand);
            Assert.AreEqual(deviceDefinitionDto.GetConfigValuesCommand, result.First().GetConfigValuesCommand);
            Assert.AreEqual(deviceDefinitionDto.GetEpcListCommand, result.First().GetEpcListCommand);
            Assert.AreEqual(deviceDefinitionDto.ResetCommand, result.First().ResetCommand);
            Assert.AreEqual(deviceDefinitionDto.UpdateFirmwareCommand, result.First().UpdateFirmwareCommand);
            Assert.AreEqual(deviceDefinitionDto.FirmwareVersion, result.First().FirmwareVersion);
        }

        [TestMethod]
        public void ServiceClientTests_GetDeviceDefinitionHappyPath()
        {
            var deviceDefinitionDto = new DeviceDefinitionsDto
            {
                Name = "HelloWorld",
                SetConfigValuesCommand = true,
                GetConfigValuesCommand = true,
                GetEpcListCommand = true,
                ResetCommand = true,
                UpdateFirmwareCommand = true,
                FirmwareVersion = "4.5",
                ConfigParameters = new Dictionary<string, string>(),
                DeviceSpecificCommandNames = new List<string>(),
                SensorReadingMeasures = new Dictionary<string, string>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(deviceDefinitionDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetDeviceDefinition("HelloWorld");

            Assert.IsNotNull(result);
            Assert.AreEqual(deviceDefinitionDto.Name, result.Name);
            Assert.AreEqual(deviceDefinitionDto.SetConfigValuesCommand, result.SetConfigValuesCommand);
            Assert.AreEqual(deviceDefinitionDto.GetConfigValuesCommand, result.GetConfigValuesCommand);
            Assert.AreEqual(deviceDefinitionDto.GetEpcListCommand, result.GetEpcListCommand);
            Assert.AreEqual(deviceDefinitionDto.ResetCommand, result.ResetCommand);
            Assert.AreEqual(deviceDefinitionDto.UpdateFirmwareCommand, result.UpdateFirmwareCommand);
            Assert.AreEqual(deviceDefinitionDto.FirmwareVersion, result.FirmwareVersion);
        }

        /// <summary>
        /// Verifies the successful retrieval of new device definition DTO 
        /// based on the old id (GUID)
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_GetNewDeviceDefinitionId()
        {
            var deviceDefinitionDto = new DeviceDefinitionsDto
            {
                Id = 1,
                Name = "HelloWorld",
                SetConfigValuesCommand = true,
                GetConfigValuesCommand = true,
                GetEpcListCommand = true,
                ResetCommand = true,
                UpdateFirmwareCommand = true,
                FirmwareVersion = "4.5",
                ConfigParameters = new Dictionary<string, string>(),
                DeviceSpecificCommandNames = new List<string>(),
                SensorReadingMeasures = new Dictionary<string, string>()
            };

            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(deviceDefinitionDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");

            var result = service.GetNewDeviceDefinitionId(Guid.NewGuid().ToString());

            Assert.IsNotNull(result);
            Assert.AreEqual(deviceDefinitionDto.Name, result.Name);
            Assert.AreEqual(deviceDefinitionDto.Id, result.Id);
        }

        #endregion

        #region MyRegion

        [TestMethod]
        public void ServiceClientTests_GetRegionsHappyPath()
        {
            var regionList = new List<string> { "US", "EU", "AP" };

            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(regionList));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetRegions();

            Assert.IsNotNull(result);
            Assert.AreEqual(regionList[0], result[0]);
            Assert.AreEqual(regionList[1], result[1]);
            Assert.AreEqual(regionList[2], result[2]);
        }

        #endregion

        #region Policy Tests

        [TestMethod]
        public void ServiceClientTests_GetPoliciesWithoutSearchAndSortHappyPath()
        {
            var policyDto = new PoliciesDto
            {
                Name = "HelloWorld",
                DeviceDefinition = "TestDeviceDefinition",
                Parameters = new Dictionary<string, string>()
            };
            var policyList = new List<PoliciesDto> { policyDto };

            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(policyList));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetPolicies();

            Assert.IsNotNull(result);
            Assert.AreEqual(policyDto.Name, result.First().Name);
            Assert.AreEqual(policyDto.DeviceDefinition, result.First().DeviceDefinition);
        }

        [TestMethod]
        public void ServiceClientTests_GetPolicyHappyPath()
        {
            var policyDto = new PoliciesDto
            {
                Name = "HelloWorld",
                DeviceDefinition = "TestDeviceDefinition",
                Parameters = new Dictionary<string, string>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(policyDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetPolicy("HelloWorld");

            Assert.IsNotNull(result);
            Assert.AreEqual(policyDto.Name, result.Name);
            Assert.AreEqual(policyDto.DeviceDefinition, result.DeviceDefinition);
        }

        [TestMethod]
        public void ServiceClientTests_AddPolicyHappyPath()
        {
            var policyDto = new PoliciesDto
            {
                Name = "HelloWorld",
                DeviceDefinition = "TestDeviceDefinition",
                Parameters = new Dictionary<string, string>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(policyDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.AddPolicy(policyDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(policyDto.Name, result.Name);
            Assert.AreEqual(policyDto.DeviceDefinition, result.DeviceDefinition);
        }

        /// <summary>
        /// Tests the update policy call
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_UpdatePolicyHappyPath()
        {
            var policyDto = new PoliciesDto
            {
                Id = 1,
                Name = "HelloWorld",
                DeviceDefinition = "TestDeviceDefinition",
                Parameters = new Dictionary<string, string>()
            };

            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(policyDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.UpdatePolicy(policyDto, "HelloWord");

            Assert.IsNotNull(result);
            Assert.AreEqual(policyDto.Id, result.Id);
            Assert.AreEqual(policyDto.Name, result.Name);
            Assert.AreEqual(policyDto.DeviceDefinition, result.DeviceDefinition);
        }

        #endregion

        #region Alias Tests

        /// <summary>
        /// Unit test for Alias methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_AddAliasHappyPath()
        {
            var aliasDto = new AliasDto
            {
                Name = "TestAlias",
                Region = "US",
                Devices = new List<string> {"TestDevice" }
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(aliasDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.AddAlias(aliasDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(aliasDto.Name, result.Name);
        }

        /// <summary>
        /// Unit test for Alias methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_ModifyAliasHappyPath()
        {
            var aliasDto = new AliasDto
            {
                Name = "TestAlias",
                Region = "US",
                Devices = new List<string> { "TestDevice" }
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(aliasDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.AddAlias(aliasDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(aliasDto.Name, result.Name);
        }

        /// <summary>
        /// Unit test for Alias methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_GetAliasesHappyPath()
        {
            var aliases = new List<AliasDto>
            {
                new AliasDto
                {
                    Name = "TestAlias",
                    Region = "US",
                    Devices = new List<string> { "TestDevice" }
                }
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(aliases));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetAliases();

            Assert.IsNotNull(result);
            Assert.AreEqual(aliases.Count, result.Count);
        }

        /// <summary>
        /// Unit test for Alias methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_GetAliasHappyPath()
        {
            var aliasDto = new AliasDto
            {
                Name = "TestAlias",
                Region = "US",
                Devices = new List<string> { "TestDevice" }
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(aliasDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetAlias("TestAlias");

            Assert.IsNotNull(result);
            Assert.AreEqual(aliasDto.Name, result.Name);
        }

        /// <summary>
        /// Unit test for Alias methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_GetAliasNamesHappyPath()
        {
            var aliasNames = new List<string>{"TestAlias"};
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(aliasNames));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetAliasNames();

            Assert.IsNotNull(result);
        }

        #endregion

        #region Device Tests

        [TestMethod]
        public void ServiceClientTests_GetDevicesWithoutSearchAndSortHappyPath()
        {
            var deviceDto = new DevicesDto
            {
                Name = "HelloWorld",
                SerialNumber = "TestSerialNumber",
                DeviceDefinition = "TestDeviceDefinition",
                Policy = "TestPolicy",
                Region = "TestRegion"
            };
            var deviceList = new List<DevicesDto>{deviceDto};
            
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(deviceList));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetDevices();

            Assert.IsNotNull(result);
            Assert.AreEqual(deviceDto.Name, result.First().Name);
            Assert.AreEqual(deviceDto.SerialNumber, result.First().SerialNumber);
            Assert.AreEqual(deviceDto.DeviceDefinition, result.First().DeviceDefinition);
            Assert.AreEqual(deviceDto.Policy, result.First().Policy);
            Assert.AreEqual(deviceDto.Region, result.First().Region);
        }

        [TestMethod]
        public void ServiceClientTests_GetDeviceHappyPath()
        {
            var deviceDto = new DevicesDto
            {
                Name = "HelloWorld",
                SerialNumber = "TestSerialNumber", 
                DeviceDefinition = "TestDeviceDefinition",
                Policy = "TestPolicy",
                Region = "TestRegion"
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(deviceDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetDevice("HelloWorld");

            Assert.IsNotNull(result);
            Assert.AreEqual(deviceDto.Name, result.Name);
            Assert.AreEqual(deviceDto.SerialNumber, result.SerialNumber);
            Assert.AreEqual(deviceDto.DeviceDefinition, result.DeviceDefinition);
            Assert.AreEqual(deviceDto.Policy, result.Policy);
            Assert.AreEqual(deviceDto.Region, result.Region);
        }

        [TestMethod]
        public void ServiceClientTests_AddDeviceHappyPath()
        {
            var deviceDto = new DevicesDto
            {
                Name = "HelloWorld",
                SerialNumber = "TestSerialNumber",
                DeviceDefinition = "TestDeviceDefinition",
                Policy = "TestPolicy",
                Region = "TestRegion"
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(deviceDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.AddDevice(deviceDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(deviceDto.Name, result.Name);
            Assert.AreEqual(deviceDto.SerialNumber, result.SerialNumber);
            Assert.AreEqual(deviceDto.DeviceDefinition, result.DeviceDefinition);
            Assert.AreEqual(deviceDto.Policy, result.Policy);
            Assert.AreEqual(deviceDto.Region, result.Region);
        }

        [TestMethod]
        public void ServiceClientTests_GetDeviceStatusHappyPath()
        {
            var statusDto = new DeviceStatusDto
            {
                WebSocketsEnabled = true
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(statusDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetDeviceStatus("HelloWorld");

            Assert.IsNotNull(result);
            Assert.AreEqual(statusDto.WebSocketsEnabled, result.WebSocketsEnabled);
        }

        #endregion

        #region Device Command Tests

        [TestMethod]
        public void ServiceClientTests_SendGetEpcListCommandHappyPath()
        {
            var commandResponse = new CommandResponseDto
            {
                CommandId = Guid.NewGuid().ToString(),
                Status = "Completed",
                ExceptionList = new List<KeyValuePair<string, string>>(),
                OutputParameterList = new List<KeyValuePair<string, string>>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(commandResponse));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.SendGetEpcListCommand("TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        [TestMethod]
        public void ServiceClientTests_SendResetCommandHappyPath()
        {
            var commandResponse = new CommandResponseDto
            {
                CommandId = Guid.NewGuid().ToString(),
                Status = "Completed",
                ExceptionList = new List<KeyValuePair<string, string>>(),
                OutputParameterList = new List<KeyValuePair<string, string>>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(commandResponse));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.SendResetCommand("TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        [TestMethod]
        public void ServiceClientTests_SendVersionCommandHappyPath()
        {
            var version = new VersionDto
            {
                Component = "AGENT",
                Url = "test://newfirmware.version"
            };
            var commandResponse = new CommandResponseDto
            {
                CommandId = Guid.NewGuid().ToString(),
                Status = "Completed",
                ExceptionList = new List<KeyValuePair<string, string>>(),
                OutputParameterList = new List<KeyValuePair<string, string>>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(commandResponse));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.SendVersionCommand("TestDevice", version);

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        [TestMethod]
        public void ServiceClientTests_SendLockdownCommandHappyPath()
        {
            var lockdown = new LockdownDto
            {
                Hours = 15
            };
            var commandResponse = new CommandResponseDto
            {
                CommandId = Guid.NewGuid().ToString(),
                Status = "Completed",
                ExceptionList = new List<KeyValuePair<string, string>>(),
                OutputParameterList = new List<KeyValuePair<string, string>>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(commandResponse));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.SendLockdownCommand("TestDevice", lockdown);

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        [TestMethod]
        public void ServiceClientTests_SendUnlockDoorCommandHappyPath()
        {
            var unlockDoor = new UnlockDoorDto
            {
                AccessToken = "0000000000"
            };
            var commandResponse = new CommandResponseDto
            {
                CommandId = Guid.NewGuid().ToString(),
                Status = "Completed",
                ExceptionList = new List<KeyValuePair<string, string>>(),
                OutputParameterList = new List<KeyValuePair<string, string>>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(commandResponse));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.SendUnlockDoorCommand("TestDevice", unlockDoor);

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        /// <summary>
        /// Verifies the successful completion of SendGetApplicationValues
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_SendGetApplicationValues()
        {
            var parameters = new List<string>{"Name1","Name2","Name3"};
            var commandResponse = new CommandResponseDto
            {
                CommandId = Guid.NewGuid().ToString(),
                Status = "Completed",
                ExceptionList = new List<KeyValuePair<string, string>>(),
                OutputParameterList = new List<KeyValuePair<string, string>>()
            };

            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(commandResponse));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.SendGetApplicationValues(parameters, "TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);

        }

        /// <summary>
        /// Verifies the successful completion of SendSetApplicationValues
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_SendSetApplicationValues()
        {
            var commandResponse = new CommandResponseDto
            {
                CommandId = Guid.NewGuid().ToString(),
                Status = "Completed",
                ExceptionList = new List<KeyValuePair<string, string>>(),
                OutputParameterList = new List<KeyValuePair<string, string>>()
            };

            var appConfig = new AppConfigValuesCommandDto
            {
                Parameters = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Test","Test")
                }
            };

            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(commandResponse));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.SendSetApplicationValues(appConfig, "TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        /// <summary>
        /// Verifies the successful completion of SendUpdateApplicationVersion
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_SendUpdateApplicationVersion()
        {
            var commandResponse = new CommandResponseDto
            {
                CommandId = Guid.NewGuid().ToString(),
                Status = "Completed",
                ExceptionList = new List<KeyValuePair<string, string>>(),
                OutputParameterList = new List<KeyValuePair<string, string>>()
            };

            var appVersion = new ApplicationVersionDto
            {
                Url = "test.com",
                Username = "User1",
                Password = "TestPass"
            };

            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(commandResponse));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.SendUpdateApplicationVersion(appVersion, "TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        #endregion

        #region Device Policy Tests
        
        [TestMethod]
        public void ServiceClientTests_GetDevicePolicyHappyPath()
        {
            var devicePolicyDto = new DevicesPolicyDto
            {
                Name = "HelloWorld",
                Parameters = new Dictionary<string, string>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(devicePolicyDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetDevicePolicy("TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(devicePolicyDto.Name, result.Name);
        }

        [TestMethod]
        public void ServiceClientTests_AddDeviceToPolicyHappyPath()
        {
            var devicePolicyDto = new DevicesPolicyDto
            {
                Name = "HelloWorld",
                Parameters = new Dictionary<string, string>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(devicePolicyDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.AddDeviceToPolicy("TestDevice", devicePolicyDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(devicePolicyDto.Name, result.Name);
        }

        [TestMethod]
        public void ServiceClientTests_SyncDevicePolicyHappyPath()
        {
            var commandResponse = new CommandResponseDto
            {
                CommandId = Guid.NewGuid().ToString(),
                Status = "Completed",
                ExceptionList = new List<KeyValuePair<string, string>>(),
                OutputParameterList = new List<KeyValuePair<string, string>>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(commandResponse));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.SyncDevicePolicy("TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        [TestMethod]
        public void ServiceClientTests_GetSyncedDevicePolicyHappyPath()
        {
            var commandResponse = new CommandResponseDto
            {
                CommandId = Guid.NewGuid().ToString(),
                Status = "Completed",
                ExceptionList = new List<KeyValuePair<string, string>>(),
                OutputParameterList = new List<KeyValuePair<string, string>>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(commandResponse));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetSyncedDevicePolicy("TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        #endregion

        #region Device Credentials Tests

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_AddDeviceCredentialsBasicHappyPath()
        {
            var credentials = new List<string> {"1234567890", "0987654321"};
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.AddDeviceCredentials("TestDevice", Enums.DeviceCredentialTypes.Basic, credentials);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials[0], result[0]);
            Assert.AreEqual(credentials[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_AddDeviceCredentialsPinHappyPath()
        {
            var credentials = new Dictionary<string, string> {{"User1", "1234"}, {"User2", "5678"} };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials.Keys.ToList()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.AddDeviceCredentials("TestDevice", Enums.DeviceCredentialTypes.Pin, credentials);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials.Keys.ToList()[0], result[0]);
            Assert.AreEqual(credentials.Keys.ToList()[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_AddDeviceCredentialsUserHappyPath()
        {
            var credentials = new Dictionary<string, string> { { "User1@stuff", "password1" }, { "User2@stuff", "mypassword" } };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials.Keys.ToList()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.AddDeviceCredentials("TestDevice", Enums.DeviceCredentialTypes.User, credentials);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials.Keys.ToList()[0], result[0]);
            Assert.AreEqual(credentials.Keys.ToList()[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_ModifyDeviceCredentialsBasicHappyPath()
        {
            var credentials = new List<string> { "1234567890", "0987654321" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.ModifyDeviceCredentials("TestDevice", Enums.DeviceCredentialTypes.Basic, credentials);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials[0], result[0]);
            Assert.AreEqual(credentials[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_ModifyDeviceCredentialsPinHappyPath()
        {
            var credentials = new Dictionary<string, string> { { "User1", "1234" }, { "User2", "5678" } };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials.Keys.ToList()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.ModifyDeviceCredentials("TestDevice", Enums.DeviceCredentialTypes.Pin, credentials);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials.Keys.ToList()[0], result[0]);
            Assert.AreEqual(credentials.Keys.ToList()[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_ModifyDeviceCredentialsUserHappyPath()
        {
            var credentials = new Dictionary<string, string> { { "User1@stuff", "password1" }, { "User2@stuff", "mypassword" } };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials.Keys.ToList()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.ModifyDeviceCredentials("TestDevice", Enums.DeviceCredentialTypes.User, credentials);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials.Keys.ToList()[0], result[0]);
            Assert.AreEqual(credentials.Keys.ToList()[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_GetDeviceCredentialsBasicHappyPath()
        {
            var credentials = new List<string> { "1234567890", "0987654321" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetDeviceCredentials("TestDevice", Enums.DeviceCredentialTypes.Basic);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials[0], result[0]);
            Assert.AreEqual(credentials[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_GetDeviceCredentialsPinHappyPath()
        {
            var credentials = new List<string>{ "User1",  "User2"};
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetDeviceCredentials("TestDevice", Enums.DeviceCredentialTypes.Pin);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials[0], result[0]);
            Assert.AreEqual(credentials[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_GetDeviceCredentialsUserHappyPath()
        {
            var credentials = new List<string>{ "User1@stuff", "User2@stuff"};
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetDeviceCredentials("TestDevice", Enums.DeviceCredentialTypes.User);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials[0], result[0]);
            Assert.AreEqual(credentials[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_DeleteDeviceCredentialsBasicHappyPath()
        {
            var credentials = new List<string> { "1234567890", "0987654321" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(new List<string>()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.DeleteDeviceCredentials("TestDevice", Enums.DeviceCredentialTypes.Basic, credentials);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(credentials.Count, result.Count);
            Assert.IsTrue(result.Count == 0);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_DeleteDeviceCredentialsPinHappyPath()
        {
            var credentials = new List<string> { "User1", "User2" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(new List<string>()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.DeleteDeviceCredentials("TestDevice", Enums.DeviceCredentialTypes.Pin, credentials);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(credentials.Count, result.Count);
            Assert.IsTrue(result.Count == 0);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_DeleteDeviceCredentialsUserHappyPath()
        {
            var credentials = new List<string> { "User1@stuff", "User2@stuff" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(new List<string>()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.DeleteDeviceCredentials("TestDevice", Enums.DeviceCredentialTypes.User, credentials);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(credentials.Count, result.Count);
            Assert.IsTrue(result.Count == 0);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_SyncDeviceCredentialsHappyPath()
        {
            var commandResponse = new CommandResponseDto
            {
                CommandId = Guid.NewGuid().ToString(),
                Status = "Completed",
                ExceptionList = new List<KeyValuePair<string, string>>(),
                OutputParameterList = new List<KeyValuePair<string, string>>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(commandResponse));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.SyncDeviceCredentials("TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public void ServiceClientTests_GetLastDeviceCredentialSyncTimeHappyPath()
        {
            var syncTime = DateTime.UtcNow;
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(syncTime));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = service.GetLastDeviceCredentialSyncTime("TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(syncTime, result);
        }

        #endregion
    }

    #region Helper Classes

    [TestClass]
    [ExcludeFromCodeCoverage]
    #pragma warning disable 1587
    /// <summary>A web request creator for unit testing.</summary>
    #pragma warning restore 1587
    public class TestWebRequestCreate : IWebRequestCreate
    {
        private static WebRequest _nextRequest;
        private static readonly object LockObject = new object();

        public static WebRequest NextRequest
        {
            get { return _nextRequest; }
            set
            {
                lock (LockObject)
                {
                    _nextRequest = value;
                }
            }
        }

        /// <summary>See <see cref="IWebRequestCreate.Create"/>.</summary>
        public WebRequest Create(Uri uri)
        {
            return _nextRequest;
        }

        /// <summary>Utility method for creating a TestWebRequest and setting 
        /// it to be the next WebRequest to use.</summary>
        /// <param name="response">The response the TestWebRequest will return.</param>
        public static TestWebRequest CreateTestRequest(string response)
        {
            TestWebRequest request = new TestWebRequest(response);
            NextRequest = request;
            return request;
        }
    }

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TestWebRequest : WebRequest
    {
        private readonly MemoryStream _requestStream = new MemoryStream();
        private readonly MemoryStream _responseStream;
        private WebHeaderCollection _headers;
        public override string Method { get; set; }
        public override string ContentType { get; set; }

        public override WebHeaderCollection Headers
        {
            get { return _headers ?? (_headers = new WebHeaderCollection()); }
            set { _headers = value; }
        }

        public override long ContentLength { get; set; }

        /// <summary>Initializes a new instance of <see cref="TestWebRequest"/> 
        /// with the response to return.</summary>
        public TestWebRequest(string response)
        {
            _responseStream = new MemoryStream(Encoding.UTF8.GetBytes(response));
        }

        /// <summary>Returns the request contents as a string.</summary>
        public string ContentAsString()
        {
            return Encoding.UTF8.GetString(_requestStream.ToArray());
        }

        /// <summary>See <see cref="WebRequest.GetRequestStream"/>.</summary>
        public override Stream GetRequestStream()
        {
            return _requestStream;
        }

        /// <summary>See <see cref="WebRequest.GetResponse"/>.</summary>
        public override WebResponse GetResponse()
        {
            return new TestWebResponse(_responseStream);
        }
    }

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TestWebResponse : WebResponse
    {
        private readonly Stream _responseStream;

        /// <summary>Initializes a new instance of <see cref="TestWebResponse"/> 
        /// with the response stream to return.</summary>
        public TestWebResponse(Stream responseStream)
        {
            _responseStream = responseStream;
        }

        /// <summary>See <see cref="WebResponse.GetResponseStream"/>.</summary>
        public override Stream GetResponseStream()
        {
            return _responseStream;
        }
    }

    #endregion
}
