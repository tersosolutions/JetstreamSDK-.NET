/*
    Copyright 2018 Terso Solutions, Inc.

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

using System.Collections.Generic;
using TersoSolutions.Jetstream.SDK.Objects;

namespace TersoSolutions.Jetstream.SDK
{
    /// <summary>
    /// A contract that specifies the properties and actions for a Jetstream client. It is
    /// intended to provide the authentication requirements and action necessary to return
    /// data from a Jetstream instance.
    /// </summary>
    public interface IJetstreamClient
    {
        /// <summary>
        /// Makes a GET request to the /events REST endpoint to retrieve 
        /// a series of events
        /// </summary>
        /// <returns>EventsDto</returns>
        EventsDto GetEvents(int limit = 100, string searchDevice = "", string searchType = "", string sortBy = "");

        /// <summary>
        /// Creates and sends a DELETE request to the /events REST endpoint that will
        /// delete events based on the batch id or array of event ids parameters.
        /// </summary>
        /// <param name="deleteEventsDto"></param>
        void DeleteEvents(DeleteEventsDto deleteEventsDto);

        /// <summary>
        /// Executes a GET request to the /devicedefinitions REST endpoint. Returns a list of device definitions
        /// in the body of the response.
        /// </summary>
        /// <returns></returns>
        List<DeviceDefinitionsDto> GetDeviceDefinitions(string searchName = "", string searchFirmwareVersion = "", string searchGetConfigValuesCommand = "",
            string searchSetConfigValuesCommand = "", string searchGetEpcListCommand = "", string searchResetCommand = "", string searchUpdateFirmwareCommand = "", string sortBy = "");

        /// <summary>
        /// Makes a GET request to the /devicedefinitions/{Name} endpoint. This method responds
        /// with a device definition matching the name, or a 404 if it is not found.
        /// </summary>
        /// <param name="deviceDefinitionName"></param>
        /// <returns></returns>
        DeviceDefinitionsDto GetDeviceDefinition(string deviceDefinitionName);

        /// <summary>
        /// Executes a POST request to the policy REST endpoint, adding a new policy to your application.
        /// </summary>
        /// <param name="policy"></param>
        /// <returns>PoliciesDto</returns>
        PoliciesDto AddPolicy(PoliciesDto policy);

        /// <summary>
        /// Executes a GET request to the /policies REST endpoint. Returns a list of policies
        /// in the body of the response.
        /// </summary>
        /// <returns></returns>
        List<PoliciesDto> GetPolicies(string searchName = "", string searchDeviceDefinition = "", string sortBy = "");

        /// <summary>
        /// Makes a GET request to the /policies/{Name} endpoint. This method responds
        /// with a policy matching the name, or a 404 if it is not found.
        /// </summary>
        /// <param name="policyName"></param>
        /// <returns></returns>
        PoliciesDto GetPolicy(string policyName);

        /// <summary>
        /// Makes a DELETE request to the policy REST endpoint to delete an existing policy from your application.
        /// </summary>
        /// <param name="policyName"></param>
        void DeletePolicy(string policyName);

        /// <summary>
        /// Makes a POST request to the /devices Jetstream REST endpoint. If successful it returns the device included
        /// in the body of the POST request.
        /// </summary>
        /// <param name="device"></param>
        /// <returns>DevicesDto</returns>
        DevicesDto AddDevice(DevicesDto device);

        /// <summary>
        /// Makes a DELETE request to the /devices/{deviceName} REST endpoint and removes a device from your application.
        /// </summary>
        /// <param name="deviceName"></param>
        void DeleteDevice(string deviceName);

        /// <summary>
        /// Makes a GET request to the /devices REST endpoint, returning a list of devices and their properties.
        /// Replaces the v1.5 GetConfiguration command
        /// </summary>
        /// <returns>List of DevicesDto</returns>
        List<DevicesDto> GetDevices(string searchName = "", string searchSerialNumber = "", string searchDeviceDefinition = "",
            string searchRegion = "", string searchPolicy = "", string sortBy = "");

        /// <summary>
        /// Makes a GET request to the /devices/{name} REST endpoint, returning a specified device.
        /// </summary>
        /// <returns>DevicesDto</returns>
        DevicesDto GetDevice(string deviceName);

        /// <summary>
        /// Get the device status
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        DeviceStatusDto GetDeviceStatus(string deviceName);

        /// <summary>
        /// Send a GetEPCListCommand to the device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        CommandResponseDto SendGetEpcListCommand(string deviceName);

        /// <summary>
        /// Send a ResetCommand to the device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        CommandResponseDto SendResetCommand(string deviceName);

        /// <summary>
        /// Send an UpdateFirmwareCommand to the device
        /// to update the firmware version of the reader or agent
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="versionDto"></param>
        /// <returns>CommandResponseDto</returns>
        CommandResponseDto SendVersionCommand(string deviceName, VersionDto versionDto);

        /// <summary>
        /// Send a LockdownCommand to the device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="lockdownDto"></param>
        /// <returns></returns>
        CommandResponseDto SendLockdownCommand(string deviceName, LockdownDto lockdownDto);

        /// <summary>
        /// Send a UnlockDoorCommand to the device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="unlockDoorDto"></param>
        /// <returns></returns>
        CommandResponseDto SendUnlockDoorCommand(string deviceName, UnlockDoorDto unlockDoorDto);

        /// <summary>
        /// Send a GetPassesCommand to the device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        CommandResponseDto SendGetAccessControlCommand(string deviceName);

        /// <summary>
        /// Send an UpdatePassesCommand to the device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="postAccessControlDto"></param>
        /// <returns></returns>
        CommandResponseDto SendPostAccessControlCommand(string deviceName, PostAccessControlDto postAccessControlDto);

        /// <summary>
        /// Send an UpdatePassesCommand to the device
        /// that removes all passes on device before adding
        /// whatever new specified passes are sent.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="putAccessControlDto"></param>
        /// <returns></returns>
        CommandResponseDto SendPutAccessControlCommand(string deviceName, PutAccessControlDto putAccessControlDto);

        /// <summary>
        /// Queues a proprietary command, whose name matches the
        /// user specified strings in the policy, queues a 
        /// CommandQueuedEvent, and returns a command response
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="commandName"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        CommandResponseDto SendProprietaryCommand(string deviceName, string commandName,
            ProprietaryCommandDto commandParameters);

        /// <summary>
        /// Creates a POST request to the /devices/{deviceName}/policy
        /// endpoint to add a device to one of your application's policies.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="devicesPolicy"></param>
        /// <returns>DevicesPolicyDto</returns>
        DevicesPolicyDto AddDeviceToPolicy(string deviceName, DevicesPolicyDto devicesPolicy);

        /// <summary>
        /// Creates a GET request to the /devices/{deviceName}/policy
        /// endpoint to get the device's policy, if there is one.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>DevicesPolicyDto</returns>
        DevicesPolicyDto GetDevicePolicy(string deviceName);

        /// <summary>
        /// Sends a DELETE request to the /devices/{deviceName}/policy
        /// REST endpoint to remove an existing device from a policy
        /// </summary>
        /// <param name="deviceName"></param>
        void RemoveDeviceFromPolicy(string deviceName);

        /// <summary>
        /// Sends the current device policy, if there is one,
        /// parameters to the device.
        /// Replaces SetConfigValuesCommand
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        CommandResponseDto SyncDevicePolicy(string deviceName);

        /// <summary>
        /// Get the currently synced device policy, if there is one.
        /// Replaces GetConfigValuesCommand
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        CommandResponseDto GetSyncedDevicePolicy(string deviceName);
    }
}
