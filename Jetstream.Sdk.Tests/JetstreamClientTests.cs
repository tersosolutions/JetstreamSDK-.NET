/*
    Copyright 2023 Terso Solutions, Inc.

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
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TersoSolutions.Jetstream.Sdk.Objects;
using TersoSolutions.Jetstream.Sdk.Objects.Events;

namespace TersoSolutions.Jetstream.Sdk.Tests
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
                Assert.IsTrue(e.Message.Contains("jetstreamUrl"));
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
        public async Task ServiceClientTests_GetEventsWithoutSearchAndSortHappyPath()
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
            var result = await service.GetEventsAsync();

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
        public async Task ServiceClientTests_GetEventsManyWithoutSearchAndSortHappyPath()
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
            var result = await service.GetEventsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(eventsDto.Count, result.Count);
            Assert.AreEqual(eventsDto.BatchId, result.BatchId);
        }

        #endregion

        #region Device Definition Tests

        [TestMethod]
        public async Task ServiceClientTests_GetDeviceDefinitionsWithoutSearchAndSortHappyPath()
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
            var result = await service.GetDeviceDefinitionsAsync();

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
        public async Task ServiceClientTests_GetDeviceDefinitionHappyPath()
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
            var result = await service.GetDeviceDefinitionAsync("HelloWorld");

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
        public async Task ServiceClientTests_GetNewDeviceDefinitionId()
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

            var result = await service.GetNewDeviceDefinitionIdAsync(Guid.NewGuid().ToString());

            Assert.IsNotNull(result);
            Assert.AreEqual(deviceDefinitionDto.Name, result.Name);
            Assert.AreEqual(deviceDefinitionDto.Id, result.Id);
        }

        #endregion

        #region Region tests

        [TestMethod]
        public async Task ServiceClientTests_GetRegionsHappyPath()
        {
            var regionList = new List<string> { "US", "EU", "AP" };

            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(regionList));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.GetRegionsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(regionList[0], result[0]);
            Assert.AreEqual(regionList[1], result[1]);
            Assert.AreEqual(regionList[2], result[2]);
        }

        #endregion

        #region Policy Tests

        [TestMethod]
        public async Task ServiceClientTests_GetPoliciesWithoutSearchAndSortHappyPath()
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
            var result = await service.GetPoliciesAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(policyDto.Name, result.First().Name);
            Assert.AreEqual(policyDto.DeviceDefinition, result.First().DeviceDefinition);
        }

        [TestMethod]
        public async Task ServiceClientTests_GetPolicyHappyPath()
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
            var result = await service.GetPolicyAsync("HelloWorld");

            Assert.IsNotNull(result);
            Assert.AreEqual(policyDto.Name, result.Name);
            Assert.AreEqual(policyDto.DeviceDefinition, result.DeviceDefinition);
        }

        [TestMethod]
        public async Task ServiceClientTests_AddPolicyHappyPath()
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
            var result = await service.AddPolicyAsync(policyDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(policyDto.Name, result.Name);
            Assert.AreEqual(policyDto.DeviceDefinition, result.DeviceDefinition);
        }

        /// <summary>
        /// Tests the update policy call
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_UpdatePolicyHappyPath()
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
            var result = await service.UpdatePolicyAsync(policyDto, "HelloWord");

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
        public async Task ServiceClientTests_AddAliasHappyPath()
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
            var result = await service.AddAliasAsync(aliasDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(aliasDto.Name, result.Name);
        }

        /// <summary>
        /// Unit test for Alias methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_ModifyAliasHappyPath()
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
            var result = await service.AddAliasAsync(aliasDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(aliasDto.Name, result.Name);
        }

        /// <summary>
        /// Unit test for Alias methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_GetAliasesHappyPath()
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
            var result = await service.GetAliasesAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(aliases.Count, result.Count);
        }

        /// <summary>
        /// Unit test for Alias methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_GetAliasHappyPath()
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
            var result = await service.GetAliasAsync("TestAlias");

            Assert.IsNotNull(result);
            Assert.AreEqual(aliasDto.Name, result.Name);
        }

        /// <summary>
        /// Unit test for Alias methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_GetAliasNamesHappyPath()
        {
            var aliasNames = new List<string> { "TestAlias" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(aliasNames));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.GetAliasNamesAsync();

            Assert.IsNotNull(result);
        }

        #endregion

        #region Device Tests

        [TestMethod]
        public async Task ServiceClientTests_GetDevicesWithoutSearchAndSortHappyPath()
        {
            var deviceDto = new DevicesDto
            {
                Name = "HelloWorld",
                SerialNumber = "TestSerialNumber",
                DeviceDefinition = "TestDeviceDefinition",
                Policy = "TestPolicy",
                Region = "TestRegion"
            };
            var deviceList = new List<DevicesDto> { deviceDto };

            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(deviceList));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.GetDevicesAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(deviceDto.Name, result.First().Name);
            Assert.AreEqual(deviceDto.SerialNumber, result.First().SerialNumber);
            Assert.AreEqual(deviceDto.DeviceDefinition, result.First().DeviceDefinition);
            Assert.AreEqual(deviceDto.Policy, result.First().Policy);
            Assert.AreEqual(deviceDto.Region, result.First().Region);
        }

        [TestMethod]
        public async Task ServiceClientTests_GetDeviceHappyPath()
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
            var result = await service.GetDeviceAsync("HelloWorld");

            Assert.IsNotNull(result);
            Assert.AreEqual(deviceDto.Name, result.Name);
            Assert.AreEqual(deviceDto.SerialNumber, result.SerialNumber);
            Assert.AreEqual(deviceDto.DeviceDefinition, result.DeviceDefinition);
            Assert.AreEqual(deviceDto.Policy, result.Policy);
            Assert.AreEqual(deviceDto.Region, result.Region);
        }

        [TestMethod]
        public async Task ServiceClientTests_AddDeviceHappyPath()
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
            var result = await service.AddDeviceAsync(deviceDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(deviceDto.Name, result.Name);
            Assert.AreEqual(deviceDto.SerialNumber, result.SerialNumber);
            Assert.AreEqual(deviceDto.DeviceDefinition, result.DeviceDefinition);
            Assert.AreEqual(deviceDto.Policy, result.Policy);
            Assert.AreEqual(deviceDto.Region, result.Region);
        }

        [TestMethod]
        public async Task ServiceClientTests_GetDeviceStatusHappyPath()
        {
            var statusDto = new DeviceStatusDto
            {
                WebSocketsEnabled = true
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(statusDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.GetDeviceStatusAsync("HelloWorld");

            Assert.IsNotNull(result);
            Assert.AreEqual(statusDto.WebSocketsEnabled, result.WebSocketsEnabled);
        }

        #endregion

        #region Device Command Tests

        [TestMethod]
        public async Task ServiceClientTests_SendGetEpcListCommandHappyPath()
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
            var result = await service.SendGetEpcListCommandAsync("TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        [TestMethod]
        public async Task ServiceClientTests_SendResetCommandHappyPath()
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
            var result = await service.SendResetCommandAsync("TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        [TestMethod]
        public async Task ServiceClientTests_SendVersionCommandHappyPath()
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
            var result = await service.SendVersionCommandAsync("TestDevice", version);

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        [TestMethod]
        public async Task ServiceClientTests_SendLockdownCommandHappyPath()
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
            var result = await service.SendLockdownCommandAsync("TestDevice", lockdown);

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        [TestMethod]
        public async Task ServiceClientTests_SendUnlockDoorCommandHappyPath()
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
            var result = await service.SendUnlockDoorCommandAsync("TestDevice", unlockDoor);

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        /// <summary>
        /// Verifies the successful completion of SendGetApplicationValues
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_SendGetApplicationValues()
        {
            var parameters = new List<string> { "Name1", "Name2", "Name3" };
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
            var result = await service.SendGetApplicationValuesAsync(parameters, "TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);

        }

        /// <summary>
        /// Verifies the successful completion of SendSetApplicationValues
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_SendSetApplicationValues()
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
            var result = await service.SendSetApplicationValuesAsync(appConfig, "TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        /// <summary>
        /// Verifies the successful completion of SendUpdateApplicationVersion
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_SendUpdateApplicationVersion()
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
            var result = await service.SendUpdateApplicationVersionAsync(appVersion, "TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        #endregion

        #region Device Policy Tests

        [TestMethod]
        public async Task ServiceClientTests_GetDevicePolicyHappyPath()
        {
            var devicePolicyDto = new DevicesPolicyDto
            {
                Name = "HelloWorld",
                Parameters = new Dictionary<string, string>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(devicePolicyDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.GetDevicePolicyAsync("TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(devicePolicyDto.Name, result.Name);
        }

        [TestMethod]
        public async Task ServiceClientTests_AddDeviceToPolicyHappyPath()
        {
            var devicePolicyDto = new DevicesPolicyDto
            {
                Name = "HelloWorld",
                Parameters = new Dictionary<string, string>()
            };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(devicePolicyDto));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.AddDeviceToPolicyAsync("TestDevice", devicePolicyDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(devicePolicyDto.Name, result.Name);
        }

        [TestMethod]
        public async Task ServiceClientTests_SyncDevicePolicyHappyPath()
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
            var result = await service.SyncDevicePolicyAsync("TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
            Assert.AreEqual(commandResponse.Status, result.Status);
        }

        [TestMethod]
        public async Task ServiceClientTests_GetSyncedDevicePolicyHappyPath()
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
            var result = await service.GetSyncedDevicePolicyAsync("TestDevice");

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
        public async Task ServiceClientTests_AddDeviceCredentialsBasicHappyPath()
        {
            var credentials = new List<string> { "1234567890", "0987654321" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.AddDeviceCredentialsAsync("TestDevice", DeviceCredentialTypes.Basic, credentials);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials[0], result[0]);
            Assert.AreEqual(credentials[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_AddDeviceCredentialsPinHappyPath()
        {
            var credentials = new Dictionary<string, string> { { "User1", "1234" }, { "User2", "5678" } };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials.Keys.ToList()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.AddDeviceCredentialsAsync("TestDevice", DeviceCredentialTypes.Pin, credentials);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials.Keys.ToList()[0], result[0]);
            Assert.AreEqual(credentials.Keys.ToList()[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_AddDeviceCredentialsUserHappyPath()
        {
            var credentials = new Dictionary<string, string> { { "User1@stuff", "password1" }, { "User2@stuff", "mypassword" } };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials.Keys.ToList()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.AddDeviceCredentialsAsync("TestDevice", DeviceCredentialTypes.User, credentials);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials.Keys.ToList()[0], result[0]);
            Assert.AreEqual(credentials.Keys.ToList()[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_ModifyDeviceCredentialsBasicHappyPath()
        {
            var credentials = new List<string> { "1234567890", "0987654321" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.ModifyDeviceCredentialsAsync("TestDevice", DeviceCredentialTypes.Basic, credentials);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials[0], result[0]);
            Assert.AreEqual(credentials[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_ModifyDeviceCredentialsPinHappyPath()
        {
            var credentials = new Dictionary<string, string> { { "User1", "1234" }, { "User2", "5678" } };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials.Keys.ToList()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.ModifyDeviceCredentialsAsync("TestDevice", DeviceCredentialTypes.Pin, credentials);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials.Keys.ToList()[0], result[0]);
            Assert.AreEqual(credentials.Keys.ToList()[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_ModifyDeviceCredentialsUserHappyPath()
        {
            var credentials = new Dictionary<string, string> { { "User1@stuff", "password1" }, { "User2@stuff", "mypassword" } };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials.Keys.ToList()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.ModifyDeviceCredentialsAsync("TestDevice", DeviceCredentialTypes.User, credentials);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials.Keys.ToList()[0], result[0]);
            Assert.AreEqual(credentials.Keys.ToList()[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_GetDeviceCredentialsBasicHappyPath()
        {
            var credentials = new List<string> { "1234567890", "0987654321" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.GetDeviceCredentialsAsync("TestDevice", DeviceCredentialTypes.Basic);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials[0], result[0]);
            Assert.AreEqual(credentials[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_GetDeviceCredentialsPinHappyPath()
        {
            var credentials = new List<string> { "User1", "User2" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.GetDeviceCredentialsAsync("TestDevice", DeviceCredentialTypes.Pin);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials[0], result[0]);
            Assert.AreEqual(credentials[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_GetDeviceCredentialsUserHappyPath()
        {
            var credentials = new List<string> { "User1@stuff", "User2@stuff" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(credentials));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.GetDeviceCredentialsAsync("TestDevice", DeviceCredentialTypes.User);

            Assert.IsNotNull(result);
            Assert.AreEqual(credentials.Count, result.Count);
            Assert.AreEqual(credentials[0], result[0]);
            Assert.AreEqual(credentials[1], result[1]);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_DeleteDeviceCredentialsBasicHappyPath()
        {
            var credentials = new List<string> { "1234567890", "0987654321" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(new List<string>()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.DeleteDeviceCredentialsAsync("TestDevice", DeviceCredentialTypes.Basic, credentials);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(credentials.Count, result.Count);
            Assert.IsTrue(result.Count == 0);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_DeleteDeviceCredentialsPinHappyPath()
        {
            var credentials = new List<string> { "User1", "User2" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(new List<string>()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.DeleteDeviceCredentialsAsync("TestDevice", DeviceCredentialTypes.Pin, credentials);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(credentials.Count, result.Count);
            Assert.IsTrue(result.Count == 0);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_DeleteDeviceCredentialsUserHappyPath()
        {
            var credentials = new List<string> { "User1@stuff", "User2@stuff" };
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(new List<string>()));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.DeleteDeviceCredentialsAsync("TestDevice", DeviceCredentialTypes.User, credentials);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(credentials.Count, result.Count);
            Assert.IsTrue(result.Count == 0);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_SyncDeviceCredentialsHappyPath()
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
            var result = await service.SyncDeviceCredentialsAsync("TestDevice");

            Assert.IsNotNull(result);
            Assert.AreEqual(commandResponse.CommandId, result.CommandId);
        }

        /// <summary>
        /// Unit test for Device Credential methods
        /// </summary>
        [TestMethod]
        public async Task ServiceClientTests_GetLastDeviceCredentialSyncTimeHappyPath()
        {
            var syncTime = DateTime.UtcNow;
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            TestWebRequestCreate.CreateTestRequest(JsonConvert.SerializeObject(syncTime));

            var service = new JetstreamClient(_accessKey, "test://MyUrl");
            var result = await service.GetLastDeviceCredentialSyncTimeAsync("TestDevice");

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
            get => _nextRequest;
            set
            {
                lock (LockObject)
                {
                    _nextRequest = value;
                }
            }
        }

        /// <summary>
        /// See <see cref="IWebRequestCreate.Create"/>.
        /// </summary>
        public WebRequest Create(Uri uri)
        {
            return _nextRequest;
        }

        /// <summary>
        /// Utility method for creating a TestWebRequest and setting it to be the next WebRequest to use.
        /// </summary>
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
            get => _headers ?? (_headers = new WebHeaderCollection());
            set => _headers = value;
        }

        public override long ContentLength { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="TestWebRequest"/> with the response to return.
        /// </summary>
        public TestWebRequest(string response)
        {
            _responseStream = new MemoryStream(Encoding.UTF8.GetBytes(response));
        }

        /// <summary>
        /// Returns the request contents as a string.
        /// </summary>
        public string ContentAsString()
        {
            return Encoding.UTF8.GetString(_requestStream.ToArray());
        }

        /// <summary>
        /// See <see cref="WebRequest.GetRequestStream"/>.
        /// </summary>
        public override Stream GetRequestStream()
        {
            return _requestStream;
        }

        public override Task<Stream> GetRequestStreamAsync()
        {
            var r = GetRequestStream();
            return Task.FromResult(r);
        }

        /// <summary>
        /// See <see cref="WebRequest.GetResponse"/>.
        /// </summary>
        public override WebResponse GetResponse()
        {
            return new TestWebResponse(_responseStream);
        }

        public override Task<WebResponse> GetResponseAsync()
        {
            var r = GetResponse();
            return Task.FromResult(r);
        }
    }

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TestWebResponse : WebResponse
    {
        private readonly Stream _responseStream;

        /// <summary>
        /// Initializes a new instance of <see cref="TestWebResponse"/> with the response stream to return.
        /// </summary>
        public TestWebResponse(Stream responseStream)
        {
            _responseStream = responseStream;
        }

        /// <summary>
        /// See <see cref="WebResponse.GetResponseStream"/>.
        /// </summary>
        public override Stream GetResponseStream()
        {
            return _responseStream;
        }
    }

    #endregion
}
