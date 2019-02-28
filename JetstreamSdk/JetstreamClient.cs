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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TersoSolutions.Jetstream.SDK.Objects;
using TersoSolutions.Jetstream.SDK.Objects.Events;

namespace TersoSolutions.Jetstream.SDK
{
    /// <summary>
    /// A class that specifies the properties and actions for a Jetstream client. It is
    /// intended to provide the authentication requirements and action necessary to return
    /// data from a Jetstream instance.
    /// </summary>
    public class JetstreamClient : IJetstreamClient
    {
        #region Fields

        // The version of Jetstream to use
        private const string JetstreamVersion = "3";
        // The root of the Jetstream API in URL form
        private readonly string _baseUri;
        // The access key to use in Jetstream calls
        private readonly string _accessKey;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accessKey"></param>
        /// <param name="jetstreamApiUrl"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public JetstreamClient(string accessKey, string jetstreamApiUrl)
        {
            if (string.IsNullOrEmpty(jetstreamApiUrl)) throw new ArgumentNullException("jetstreamApiUrl");
            if (string.IsNullOrEmpty(accessKey)) throw new ArgumentNullException("accessKey");

            // normalize the url with a trail /
            jetstreamApiUrl = jetstreamApiUrl.Trim();
            if (!jetstreamApiUrl.EndsWith("/", StringComparison.InvariantCulture))
            {
                jetstreamApiUrl += "/";
            }

            // Add the Jetstream version to the base url
            jetstreamApiUrl += JetstreamVersion + "/";

            _baseUri = jetstreamApiUrl;
            _accessKey = accessKey;
        }

        #endregion

        #region Events Methods

        /// <summary>
        /// Makes a GET request to the /events endpoint to retrieve 
        /// a series of events
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="searchDevice"></param>
        /// <param name="searchType"></param>
        /// <param name="sortBy"></param>
        /// <returns>EventsDto</returns>
        public EventsDto GetEvents(int limit = 100, string searchDevice = "", string searchType = "", string sortBy = "")
        {
            // Append the events resource location to the url
            // and add the maximum limit of events to return
            // to the url
            var url = _baseUri + "events/" + limit + "?";

            var queryParams = new Dictionary<string, string>
            {
                {"device", searchDevice}, 
                {"type", searchType}, 
                {"sort", sortBy}
            };

            url += BuildQueryString(queryParams);

            return SendRequestToJetstream<string, EventsDto>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a DELETE request to the /events endpoint that will
        /// delete events based on the batch id or array of event ids parameters.
        /// </summary>
        /// <param name="deleteEventsDto"></param>
        public void DeleteEvents(DeleteEventsDto deleteEventsDto)
        {
            // Append the events resource location to the url.
            var url = _baseUri + "events";

            // Make call. If no error thrown, we assume successful
            SendRequestToJetstream<DeleteEventsDto, string>(url, "DELETE", deleteEventsDto);
        }

        #endregion

        #region Device Definition Methods

        /// <summary>
        /// Makes a GET request to the /devicedefinitions endpoint. Returns a list of device definitions
        /// in the body of the response.
        /// </summary>
        /// <param name="searchId"></param>
        /// <param name="searchName"></param>
        /// <param name="searchFirmwareVersion"></param>
        /// <param name="searchGetConfigValuesCommand"></param>
        /// <param name="searchSetConfigValuesCommand"></param>
        /// <param name="searchGetEpcListCommand"></param>
        /// <param name="searchResetCommand"></param>
        /// <param name="searchUpdateFirmwareCommand"></param>
        /// <param name="sortBy"></param>
        /// <returns>List of DeviceDefinitionsDto</returns>
        public List<DeviceDefinitionsDto> GetDeviceDefinitions(string searchId = "", string searchName = "", string searchFirmwareVersion = "", string searchGetConfigValuesCommand = "", 
            string searchSetConfigValuesCommand = "", string searchGetEpcListCommand = "", string searchResetCommand = "", string searchUpdateFirmwareCommand = "", string sortBy = "")
        {
            // Append the device definitions resource location to the url.
            var url = _baseUri + "devicedefinitions?";

            // builds a dictionary to hold parameters
            var queryParams = new Dictionary<string, string>
            {
                {"id", searchId},
                {"Name", searchName},
                {"FirmwareVersion", searchFirmwareVersion},
                {"GetConfigValuesCommand", searchGetConfigValuesCommand},
                {"SetConfigValuesCommand", searchSetConfigValuesCommand},
                {"UpdateFirmwareCommand", searchUpdateFirmwareCommand},
                {"GetEpcListCommand", searchGetEpcListCommand},
                {"ResetCommand", searchResetCommand},
                {"Sort", sortBy}
            };

            // Adds query string if there is one to the url
            url += BuildQueryString(queryParams);

            return SendRequestToJetstream<string, List<DeviceDefinitionsDto>>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a GET request to the /devicedefinitions/{Name} endpoint. This method responds
        /// with a device definition matching the name, or a 404 if it is not found.
        /// </summary>
        /// <param name="deviceDefinitionName"></param>
        /// <returns>DeviceDefinitionsDto</returns>
        public DeviceDefinitionsDto GetDeviceDefinition(string deviceDefinitionName)
        {
            // Add the device definitions resource location to the url.
            var url = _baseUri + "devicedefinitions/" + deviceDefinitionName;

            return SendRequestToJetstream<string, DeviceDefinitionsDto>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a GET request to the /devicedefinitions/OldId/{guid} endpoint. Returns new device
        /// definition object that contains the autogenerated integer id.
        /// </summary>
        /// <param name="oldId"></param>
        /// <returns>DeviceDefinitionsDto</returns>
        public DeviceDefinitionsDto GetNewDeviceDefinitionId(string oldId)
        {
            var url = _baseUri + "devicedefinitions/OldId/" + oldId;

            return SendRequestToJetstream<string, DeviceDefinitionsDto>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Region Methods

        public List<string> GetRegions()
        {
            // Append the regions resource location to the url.
            var url = _baseUri + "regions";
            
            return SendRequestToJetstream<string, List<string>>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Policy Methods

        /// <summary>
        /// Makes a POST request to the /policies endpoint, adding a new policy.
        /// If successful it returns information about the new policy.
        /// </summary>
        /// <param name="policy"></param>
        /// <returns>PoliciesDto</returns>
        public PoliciesDto AddPolicy(PoliciesDto policy)
        {
            // Append the policies resource location to the url.
            var url = _baseUri + "policies";

            return SendRequestToJetstream<PoliciesDto, PoliciesDto>(url, WebRequestMethods.Http.Post, policy);
        }

        /// <summary>
        /// Makes a GET request to the /policies endpoint. Returns a list of policies
        /// in the body of the response.
        /// </summary>
        /// <param name="searchId"></param>
        /// <param name="searchName"></param>
        /// <param name="searchDeviceDefinition"></param>
        /// <param name="sortBy"></param>
        /// <returns>List of PoliciesDTO</returns>
        public List<PoliciesDto> GetPolicies(string searchId = "", string searchName = "",
            string searchDeviceDefinition = "", string sortBy = "")
        {
            // Append the policies resource location to the url.
            var url = _baseUri + "policies?";

            // builds a dictionary to hold parameters
            var queryParams = new Dictionary<string, string>
            {
                {"id", searchId},
                {"name", searchName},
                {"devicedefinition", searchDeviceDefinition},
                {"sortby", sortBy}
            };

            // Adds query string if there is one to the url
            url += BuildQueryString(queryParams);

            return SendRequestToJetstream<string, List<PoliciesDto>>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a GET request to the /policies/{Name} endpoint. This method responds
        /// with a policy matching the name, or a 404 if it is not found.
        /// </summary>
        /// <param name="policyName"></param>
        /// <returns>PoliciesDTO</returns>
        public PoliciesDto GetPolicy(string policyName)
        {
            // Add the policies resource location to the url.
            var url = _baseUri + "policies/" + policyName;

            return SendRequestToJetstream<string, PoliciesDto>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a DELETE request to the /policies/{Name} endpoint to delete an existing policy.
        /// </summary>
        /// <param name="policyName"></param>
        public void DeletePolicy(string policyName)
        {
            // Append the policies resource location to the url.
            var url = _baseUri + "policies/" + policyName;

            // Make call. If no error thrown, we assume successful
            SendRequestToJetstream<string, string>(url, "DELETE", "");
        }

        /// <summary>
        /// Makes a PUT request to the /policies/{Name} endpoint to update an existing policy.
        /// If successful it returns the current state of the policy.
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="policyName"></param>
        /// <returns></returns>
        public PoliciesDto UpdatePolicy(PoliciesDto policy, string policyName)
        {
            // Append the policies resource location to the url.
            var url = _baseUri + "policies/" + policyName;

            return SendRequestToJetstream<PoliciesDto, PoliciesDto>(url, WebRequestMethods.Http.Put, policy);
        }
        #endregion

        #region Alias Methods

        /// <summary>
        /// Makes a POST request to the /aliases endpoint to create an alias. If successful it returns the device
        /// included in the body of the POST request.
        /// </summary>
        /// <param name="alias"></param>
        public AliasDto AddAlias(AliasDto alias)
        {
            // Append the alias resource location to the url.
            var url = _baseUri + "aliases";

            return SendRequestToJetstream<AliasDto, AliasDto>(url, WebRequestMethods.Http.Post, alias);
        }

        /// <summary>
        /// Makes a PUT request to the /aliases/{Name} endpoint to create an alias. If successful it returns 
        /// the current state of the alias in Jetstream.
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="aliasWithNewValues"></param>
        public AliasDto ModifyAlias(string aliasName, AliasDto aliasWithNewValues)
        {
            // Append the alias resource location to the url.
            var url = _baseUri + "aliases/" + aliasName;

            return SendRequestToJetstream<AliasDto, AliasDto>(url, WebRequestMethods.Http.Put, aliasWithNewValues);
        }

        /// <summary>
        /// Makes a GET request to the /aliases endpoint, returning all aliases.
        /// </summary>
        /// <param name="searchId"></param>
        /// <param name="searchName"></param>
        /// <param name="searchRegion"></param>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        public List<AliasDto> GetAliases(string searchId = "", string searchName = "", string searchRegion = "", string sortBy = "")
        {
            // Append the alias resource location to the url.
            var url = _baseUri + "aliases?";

            // builds a dictionary to hold parameters
            var queryParams = new Dictionary<string, string>
            {
                {"id", searchId}, 
                {"name", searchName}, 
                {"region", searchRegion}, 
                {"sort", sortBy}
            };

            // Adds query string if there is one to the url
            url += BuildQueryString(queryParams);

            return SendRequestToJetstream<string, List<AliasDto>>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a GET request to the /aliases/{Name} endpoint, asking for a 
        /// specific alias by name. Returns the requested alias, if it exists.
        /// </summary>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        public AliasDto GetAlias(string aliasName)
        {
            // Append the alias resource location to the url.
            var url = _baseUri + "aliases/" + aliasName;

            return SendRequestToJetstream<string, AliasDto>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a GET request to the aliases/property/names endpoint, returning the
        /// names of all aliases. Does not return any other data on the aliases.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAliasNames()
        {
            // Append the alias resource location to the url.
            var url = _baseUri + "aliases/property/names";

            return SendRequestToJetstream<string, List<string>>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a DELETE request to the /aliases/{Name} endpoint, deleting
        /// the requested alias.
        /// </summary>
        /// <param name="aliasName"></param>
        public void DeleteAlias(string aliasName)
        {
            // Append the alias resource location to the url.
            var url = _baseUri + "aliases/" + aliasName;

            SendRequestToJetstream<string, string>(url, "DELETE", string.Empty);
        }

        #endregion

        #region Device Methods

        /// <summary>
        /// Makes a POST request to the /devices Jetstream endpoint to create a new device in your application.
        /// If successful it returns the new device's information.
        /// </summary>
        /// <param name="device"></param>
        /// <returns>DevicesDto</returns>
        public DevicesDto AddDevice(DevicesDto device)
        {
            // Append the devices resource location to the url.
            var url = _baseUri + "devices";

            return SendRequestToJetstream<DevicesDto, DevicesDto>(url, WebRequestMethods.Http.Post, device);
        }

        /// <summary>
        /// Makes a DELETE request to the /devices/{deviceName} endpoint and removes a device from your application.
        /// </summary>
        /// <param name="deviceName"></param>
        public void DeleteDevice(string deviceName)
        {
            // Append the devices resource location to the url.
            var url = _baseUri + "devices/" + deviceName;

            // Make call. If no error thrown, we assume successful
            SendRequestToJetstream<string, string>(url, "DELETE", "");
        }

        /// <summary>
        /// Makes a PUT request to the /devices/{deviceName} Jetstream endpoint to update an existing device.
        /// If successful it returns the current state of the device in Jetstream.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="device"></param>
        /// <returns>DevicesDto</returns>
        public DevicesDto ModifyDevice(string deviceName, DevicesDto device)
        {
            // Append the devices resource location to the url.
            var url = _baseUri + "devices/" + deviceName;

            return SendRequestToJetstream<DevicesDto, DevicesDto>(url, WebRequestMethods.Http.Put, device);
        }

        /// <summary>
        /// Makes a GET request to the /devices endpoint, returning a list of devices and their properties.
        /// </summary>
        /// <param name="searchId"></param>
        /// <param name="searchName"></param>
        /// <param name="searchSerialNumber"></param>
        /// <param name="searchDeviceDefinition"></param>
        /// <param name="searchRegion"></param>
        /// <param name="searchPolicy"></param>
        /// <param name="sortBy"></param>
        /// <returns>List of DevicesDto</returns>
        public List<DevicesDto> GetDevices(string searchId = "", string searchName = "", string searchSerialNumber = "", string searchDeviceDefinition = "", 
            string searchRegion = "", string searchPolicy = "", string sortBy = "")
        {
            // Append the devices resource location to the url.
            var url = _baseUri + "devices?";

            // builds a dictionary to hold parameters
            var queryParams = new Dictionary<string, string>
            {
                {"id", searchId},
                {"name", searchName},
                {"serialnumber", searchSerialNumber},
                {"devicedefinition", searchDeviceDefinition},
                {"region", searchRegion},
                {"sort", sortBy}
            };

            // Adds query string if there is one to the url
            url += BuildQueryString(queryParams);

            return SendRequestToJetstream<string, List<DevicesDto>>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a GET request to the /devices/{name} endpoint, returning a specified device.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>DevicesDto</returns>
        public DevicesDto GetDevice(string deviceName)
        {
            // Append the devices resource location to the url.
            var url = _baseUri + "devices/" + deviceName;

            return SendRequestToJetstream<string, DevicesDto>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a GET request to the /devices/{deviceName}/status endpoint
        /// to retrieve the status of the specified device.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>DeviceStatusDto</returns>
        public DeviceStatusDto GetDeviceStatus(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/status";

            return SendRequestToJetstream<string, DeviceStatusDto>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Device Commands

        /// <summary>
        /// Makes a GET request to the /devices/{deviceName}/epclist endpoint.
        /// This endpoint triggers a device to perform an Object Event scan.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendGetEpcListCommand(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/epclist";

            return SendRequestToJetstream<string, CommandResponseDto>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a Post request to the /devices/{deviceName}/reset endpoint.
        /// This endpoint triggers the device to restart.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendResetCommand(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/reset";

            return SendRequestToJetstream<string, CommandResponseDto>(url, WebRequestMethods.Http.Post, string.Empty);
        }

        /// <summary>
        /// Makes a POST request to the /devices/{deviceName}/version endpoint.
        /// This is used to update the firmware version of the reader or agent.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="versionDto"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendVersionCommand(string deviceName, VersionDto versionDto)
        {
            var url = _baseUri + "devices/" + deviceName + "/version";

            return SendRequestToJetstream<VersionDto, CommandResponseDto>(url, WebRequestMethods.Http.Post, versionDto);
        }

        /// <summary>
        /// Makes a POST request to the /devices/{deviceName}/lockdown endpoint.
        /// This endpoint triggers the device to go into lockdown.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="lockdownDto"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendLockdownCommand(string deviceName, LockdownDto lockdownDto)
        {
            var url = _baseUri + "devices/" + deviceName + "/lockdown";

            return SendRequestToJetstream<LockdownDto, CommandResponseDto>(url, WebRequestMethods.Http.Post, lockdownDto);
        }

        /// <summary>
        /// Makes a POST request to the /devices/{deviceName}/unlockdoor endpoint.
        /// This endpoint triggers the door to unlock on a device.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="unlockDoorDto"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendUnlockDoorCommand(string deviceName, UnlockDoorDto unlockDoorDto)
        {
            var url = _baseUri + "devices/" + deviceName + "/unlockdoor";

            return SendRequestToJetstream<UnlockDoorDto, CommandResponseDto>(url, WebRequestMethods.Http.Post, unlockDoorDto);
        }
        
        /// <summary>
        /// Makes a GET request the /devices/{deviceName}/ApplicationValues endpoint.
        /// This endpoint instructs the specified application on a device to return the current values for its configuration parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        public CommandResponseDto SendGetApplicationValues(List<string> parameters, string deviceName)
        {
            var joinedParameters = string.Join(",", parameters);

            // Append the resource to the url.
            var url = _baseUri + "devices/" + deviceName + "/ApplicationValues?parameters=" + joinedParameters;

            return SendRequestToJetstream<string, CommandResponseDto>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a POST request the /devices/{deviceName}/ApplicationValues endpoint.
        /// This endpoint instructs the specified application on a device to set the values for its configuration parameters.
        /// </summary>
        /// <param name="appConfigValuesDto"></param>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        public CommandResponseDto SendSetApplicationValues(AppConfigValuesCommandDto appConfigValuesDto, string deviceName)
        {
            // Append the resource to the url.
            var url = _baseUri + "devices/" + deviceName + "/ApplicationValues";

            return SendRequestToJetstream<AppConfigValuesCommandDto, CommandResponseDto>(url, WebRequestMethods.Http.Post, appConfigValuesDto);
        }

        /// <summary>
        /// Makes a POST request to the /devices/{deviceName}/ApplicationVersion endpoint.
        /// This endpoint instructs the application running on a device to update using the url provided.
        /// </summary>
        /// <param name="appVersion"></param>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        public CommandResponseDto SendUpdateApplicationVersion(ApplicationVersionDto appVersion, string deviceName)
        {
            // Append the resource to the url.
            var url = _baseUri + "devices/" + deviceName + "/ApplicationVersion";

            return SendRequestToJetstream<ApplicationVersionDto, CommandResponseDto>(url, WebRequestMethods.Http.Post, appVersion);
        }

        #endregion

        #region Device Policy
        /// <summary>
        /// Makes a POST request to the /devices/{deviceName}/policy
        /// endpoint to add a device to one of your application's policies.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="devicesPolicy"></param>
        /// <returns>DevicesPolicyDto</returns>
        public DevicesPolicyDto AddDeviceToPolicy(string deviceName, DevicesPolicyDto devicesPolicy)
        {
            // Append the devices resource location to the url.
            var url = _baseUri + "devices/" + deviceName + "/policy";

            return SendRequestToJetstream<DevicesPolicyDto, DevicesPolicyDto>(url, WebRequestMethods.Http.Post, devicesPolicy);
        }

        /// <summary>
        /// Makes a GET request to the /devices/{deviceName}/policy
        /// endpoint to get the device's policy, if there is one.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>DevicesPolicyDto</returns>
        public DevicesPolicyDto GetDevicePolicy(string deviceName)
        {
            // Append the devices resource location to the url.
            var url = _baseUri + "devices/" + deviceName + "/policy";

            return SendRequestToJetstream<string, DevicesPolicyDto>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a DELETE request to the /devices/{deviceName}/policy
        /// endpoint to remove an existing device from a policy.
        /// </summary>
        /// <param name="deviceName"></param>
        public void RemoveDeviceFromPolicy(string deviceName)
        {
            // Append the devices resource location to the url.
            var url = _baseUri + "devices/" + deviceName + "/policy";

            // Make call and if no error is thrown we assume success
            SendRequestToJetstream<string, string>(url, "DELETE", "");
        }

        /// <summary>
        /// Makes a POST request to the /devices/{deviceName}/policy/sync endpoint.
        /// The endpoint causes the current device policy to be sent to the device.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SyncDevicePolicy(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/policy/sync";
            return SendRequestToJetstream<string, CommandResponseDto>(url, WebRequestMethods.Http.Post, string.Empty);
        }

        /// <summary>
        /// Makes a GET request to /devices/{deviceName}/policy/sync endpoint.
        /// This endpoint causes the device to send back the current policy is it synced with if any.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto GetSyncedDevicePolicy(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/policy/sync";
            return SendRequestToJetstream<string, CommandResponseDto>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Device Credentials Commands

        /// <summary>
        /// Makes a POST request to the /devices/{deviceName}/credentials/{credentialType} endpoint,
        /// appending the passed in credentials to the specified type's list.
        /// Returns the current state of the credentials.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="type"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public List<string> AddDeviceCredentials(string deviceName, Enums.DeviceCredentialTypes type, List<string> credentials)
        {
            // Append the device credential resource location to the url.
            var url = _baseUri + "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(Enums.DeviceCredentialTypes), type);

            return SendRequestToJetstream<List<string>, List<string>>(url, WebRequestMethods.Http.Post, credentials);
        }

        /// <summary>
        /// Makes a POST request to the /devices/{deviceName}/credentials/{credentialType} endpoint,
        /// appending the passed in credentials to the specified type's dictionary.
        /// Returns the current state of the credentials keys.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="type"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public List<string> AddDeviceCredentials(string deviceName, Enums.DeviceCredentialTypes type, Dictionary<string, string> credentials)
        {
            // Append the device credential resource location to the url.
            var url = _baseUri + "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(Enums.DeviceCredentialTypes), type);

            return SendRequestToJetstream<Dictionary<string, string>, List<string>>(url, WebRequestMethods.Http.Post, credentials);
        }

        /// <summary>
        /// Makes a PUT request to the /devices/{deviceName}/credentials/{credentialType} endpoint,
        /// overwriting the specified type's existing list with the passed in list.
        /// Returns the current state of the credentials.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="type"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public List<string> ModifyDeviceCredentials(string deviceName, Enums.DeviceCredentialTypes type, List<string> credentials)
        {
            // Append the device credential resource location to the url.
            var url = _baseUri + "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(Enums.DeviceCredentialTypes), type);

            return SendRequestToJetstream<List<string>, List<string>>(url, WebRequestMethods.Http.Put, credentials);
        }

        /// <summary>
        /// Makes a PUT request to the /devices/{deviceName}/credentials/{credentialType} endpoint, overwriting 
        /// the specified type's existing dictionary with the passed in dictionary.
        /// Returns the current state of the credentials keys.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="type"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public List<string> ModifyDeviceCredentials(string deviceName, Enums.DeviceCredentialTypes type, Dictionary<string, string> credentials)
        {
            // Append the device credential resource location to the url.
            var url = _baseUri + "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(Enums.DeviceCredentialTypes), type);

            return SendRequestToJetstream<Dictionary<string, string>, List<string>>(url, WebRequestMethods.Http.Put, credentials);
        }

        /// <summary>
        /// Makes a GET request to the /devices/{deviceName}/credentials/{credentialType} endpoint,
        /// returning the specified credentials (keys only, if dictionary) from the
        /// specified type's existing list or dictionary.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<string> GetDeviceCredentials(string deviceName, Enums.DeviceCredentialTypes type)
        {
            // Append the device credential resource location to the url.
            var url = _baseUri + "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(Enums.DeviceCredentialTypes), type);

            return SendRequestToJetstream<string, List<string>>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <summary>
        /// Makes a DELETE request to the /devices/{deviceName}/credentials/{credentialType} endpoint,
        /// removing the specified credentials (by key, if dictionary) from the
        /// specified type's existing list or dictionary.
        /// Returns the current state of the credentials.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="type"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public List<string> DeleteDeviceCredentials(string deviceName, Enums.DeviceCredentialTypes type, List<string> credentials)
        {
            // Append the device credential resource location to the url.
            var url = _baseUri + "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(Enums.DeviceCredentialTypes), type);

            return SendRequestToJetstream<List<string>, List<string>>(url, "DELETE", credentials);
        }

        /// <summary>
        /// Makes a POST request to the /devices/{deviceName}/credentials/sync sync endpoint,
        /// sending a command to the specified device to update its credentials.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        public CommandResponseDto SyncDeviceCredentials(string deviceName)
        {
            // Append the device credential resource location to the url.
            var url = _baseUri + "devices/" + deviceName + "/credentials/sync";

            return SendRequestToJetstream<string, CommandResponseDto>(url, WebRequestMethods.Http.Post, string.Empty);
        }

        /// <summary>
        /// Makes a GET request to the /devices/{deviceName}/credentials/Sync sync endpoint,
        /// returning the DateTime of the last credential sync.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        public DateTime GetLastDeviceCredentialSyncTime(string deviceName)
        {
            // Append the device credential resource location to the url.
            var url = _baseUri + "devices/" + deviceName + "/credentials/sync";

            return SendRequestToJetstream<string, DateTime>(url, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Helper method to serialize object into a request
        /// body and make a call to Jetstream
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="body"></param>
        /// <param name="httpVerb"></param>
        /// <returns>T</returns>
        private T2 SendRequestToJetstream<T1, T2>(string requestUri, string httpVerb, T1 body)
        {
            // Set security to TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Create request
            var request = WebRequest.Create(requestUri);
            // Add access key to header
            request.Headers.Add("AccessKey:" + _accessKey);
            // Specify content type as JSON
            request.ContentType = "application/json";
            // Set the verb
            request.Method = httpVerb;

            // If it's not a GET request, set the body of the request
            if (!string.Equals(httpVerb, WebRequestMethods.Http.Get))
            {
                // Set the body to an encoded object
                request = CreateRequestBody(request, JsonConvert.SerializeObject(body));
            }

            try
            {
                // Make call and get response
                var response = request.GetResponse();
                
                // Get response stream
                // ReSharper disable once AssignNullToNotNullAttribute
                var responseStreamReader = new StreamReader(response.GetResponseStream());
                // Read response stream
                var responseString = responseStreamReader.ReadToEnd();

                // Close request and close stream
                responseStreamReader.Close();
                response.Close();

                // Return DTO returned from Jetstream
                return JsonConvert.DeserializeObject<T2>(responseString, new EventDtoConverter());
            }
            catch (WebException ex)
            {
                // Cast as a http web response, if possible
                var httpResponse = ex.Response as HttpWebResponse;
                if (httpResponse == null) throw;

                // Grab the status code
                var statusCode = httpResponse.StatusCode;
                // Get the JSON of the response
                var responseBody = new StreamReader(httpResponse.GetResponseStream()).ReadToEnd();
                // Format the JSON body of the response to make a pretty error message
                var formattedMessage = CreateErrorMessageFromJsonBody(responseBody);
                // Create new Jetstream exception and throw it
                var jetstreamException = new JetstreamException(statusCode, JsonConvert.SerializeObject(body), responseBody, formattedMessage, ex);
                throw jetstreamException;
            }
        }

        /// <summary>
        /// Encode the body of a request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="body"></param>
        /// <returns>WebRequest</returns>
        private static WebRequest CreateRequestBody(WebRequest request, string body)
        {
            var postData = Encoding.Default.GetBytes(body);

            request.ContentLength = postData.Length;

            using (var s = request.GetRequestStream())
            {
                s.Write(postData, 0, postData.Length);
            }
            return request;
        }

        /// <summary>
        /// Take the unformatted JSON of the response and 
        /// create a prettier error message
        /// </summary>
        /// <param name="unformattedBody"></param>
        /// <returns></returns>
        private static string CreateErrorMessageFromJsonBody(string unformattedBody)
        {
            var formattedMessage = string.Empty;
            var responseObject = new JObject();

            // Try to cast the response as a JObject. It will work if the response is JSON.
            try
            {
                responseObject = JsonConvert.DeserializeObject<JObject>(unformattedBody);
            }
            catch (Exception)
            {
                try
                {
                    // Response was not JSON. So just make it a string and return it.
                    formattedMessage = JsonConvert.DeserializeObject<string>(unformattedBody);
                    return formattedMessage;
                }
                catch (Exception)
                {
                    // Swallowing it
                }
            }
            

            // Grab the message from the body, if it exists
            if (responseObject["Message"] != null)
            {
                formattedMessage += responseObject["Message"].ToString();
            }

            // Grab and aggregate the model state errors, if they exist
            if (responseObject["ModelState"] != null)
            {
                if (!string.IsNullOrEmpty(formattedMessage))
                {
                    formattedMessage += " | ";
                }

                // Take the ModelState section of the JSON and convert it to a JToken.
                var modelStateJToken = responseObject["ModelState"];
                // Take the JToken and convert it to a Dictionary<string, List<string>>
                var modelStateDictionary = modelStateJToken.ToObject<Dictionary<string, List<string>>>();
                // Go through each error and join them onto the string with a comma between them
                formattedMessage += string.Join(", ", modelStateDictionary.Values.Select(x => string.Join(", ", x)));
            }

            // Return the message
            return formattedMessage;
        }

        /// <summary>
        /// Builds a query string based on the
        /// parameters that are passed in.
        /// Ignores empty values.
        /// </summary>
        /// <param name="queryParams">Dictionary of parameter
        /// name and the value to be assigned to it</param>
        /// <returns>a string that represents the formatted query string</returns>
        private static string BuildQueryString(Dictionary<string, string> queryParams)
        {
            // Sets up a list of formatted query string values
            var listOfParams = (from p in queryParams
                where !string.IsNullOrEmpty(p.Value)
                select string.Format("{0}={1}", p.Key.ToLower(), p.Value)).ToList();

            // returns the query string
            return string.Join("&", listOfParams);
        }
        #endregion
    }
}
