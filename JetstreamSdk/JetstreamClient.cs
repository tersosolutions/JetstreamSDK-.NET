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
        private const string JetstreamVersion = "2";
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
            if (String.IsNullOrEmpty(jetstreamApiUrl)) throw new ArgumentNullException("jetstreamApiUrl");
            if (String.IsNullOrEmpty(accessKey)) throw new ArgumentNullException("accessKey");

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
        /// Makes a GET request to the /events REST endpoint to retrieve 
        /// a series of events
        /// </summary>
        /// <returns>EventsDto</returns>
        public EventsDto GetEvents(int limit = 100, string searchDevice = "", string searchType = "", string sortBy = "")
        {
            // Append the events resource location to the url
            // and add the maximum limit of events to return
            // to the url
            var url = _baseUri + "events/" + limit + "?";

            url += String.IsNullOrEmpty(searchDevice) ? String.Empty : "device=" + searchDevice; // Add search string
            url += String.IsNullOrEmpty(searchType) ? String.Empty : "&type=" + searchType; // Add search string
            url += String.IsNullOrEmpty(sortBy) ? String.Empty : "&sort=" + sortBy; // Add sort string

            return SendRequestToJetstream<string, EventsDto>(url, WebRequestMethods.Http.Get, String.Empty);
        }

        /// <summary>
        /// Creates and sends a DELETE request to the /events REST endpoint that will
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
        /// Executes a GET request to the /devicedefinitions REST endpoint. Returns a list of device definitions
        /// in the body of the response.
        /// </summary>
        /// <returns>List of DeviceDefinitionsDto</returns>
        public List<DeviceDefinitionsDto> GetDeviceDefinitions(string searchName = "", string searchFirmwareVersion = "", string searchGetConfigValuesCommand = "", 
            string searchSetConfigValuesCommand = "", string searchGetEpcListCommand = "", string searchResetCommand = "", string searchUpdateFirmwareCommand = "", string sortBy = "")
        {
            // Append the device definitions resource location to the url.
            var url = _baseUri + "devicedefinitions?";

            url += String.IsNullOrEmpty(searchName) ? String.Empty : "Name=" + searchName; // Add search string
            url += String.IsNullOrEmpty(searchFirmwareVersion) ? String.Empty : "&FirmwareVersion=" + searchFirmwareVersion; // Add search string
            url += String.IsNullOrEmpty(searchGetConfigValuesCommand) ? String.Empty : "&GetConfigValuesCommand=" + searchGetConfigValuesCommand; // Add search string
            url += String.IsNullOrEmpty(searchSetConfigValuesCommand) ? String.Empty : "&SetConfigValuesCommand=" + searchSetConfigValuesCommand; // Add search string
            url += String.IsNullOrEmpty(searchGetEpcListCommand) ? String.Empty : "&GetEpcListCommand=" + searchGetEpcListCommand; // Add search string
            url += String.IsNullOrEmpty(searchResetCommand) ? String.Empty : "&ResetCommand=" + searchResetCommand; // Add search string
            url += String.IsNullOrEmpty(searchUpdateFirmwareCommand) ? String.Empty : "&UpdateFirmwareCommand=" + searchUpdateFirmwareCommand; // Add search string
            url += String.IsNullOrEmpty(sortBy) ? String.Empty : "&Sort=" + sortBy; // Add sort string

            return SendRequestToJetstream<string, List<DeviceDefinitionsDto>>(url, WebRequestMethods.Http.Get, String.Empty);
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

            return SendRequestToJetstream<string, DeviceDefinitionsDto>(url, WebRequestMethods.Http.Get, String.Empty);
        }

        #endregion

        #region Policy Methods

        /// <summary>
        /// Executes a POST request to the policy REST endpoint, adding a new policy to your application.
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
        /// Executes a GET request to the /policies REST endpoint. Returns a list of policies
        /// in the body of the response.
        /// </summary>
        /// <returns>List of PoliciesDTO</returns>
        public List<PoliciesDto> GetPolicies(string searchName = "", string searchDeviceDefinition = "", string sortBy = "")
        {
            // Append the policies resource location to the url.
            var url = _baseUri + "policies?";

            url += String.IsNullOrEmpty(searchName) ? String.Empty : "name=" + searchName; // Add search string
            url += String.IsNullOrEmpty(searchDeviceDefinition) ? String.Empty : "&devicedefinition=" + searchDeviceDefinition; // Add search string
            url += String.IsNullOrEmpty(sortBy) ? String.Empty : "&sortby=" + sortBy; // Add search string

            return SendRequestToJetstream<string, List<PoliciesDto>>(url, WebRequestMethods.Http.Get, String.Empty);
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

            return SendRequestToJetstream<string, PoliciesDto>(url, WebRequestMethods.Http.Get, String.Empty);
        }

        /// <summary>
        /// Makes a DELETE request to the policy REST endpoint to delete an existing policy from your application.
        /// </summary>
        /// <param name="policyName"></param>
        public void DeletePolicy(string policyName)
        {
            // Append the policies resource location to the url.
            var url = _baseUri + "policies/" + policyName;

            // Make call. If no error thrown, we assume successful
            SendRequestToJetstream<string, string>(url, "DELETE", "");
        }

        #endregion

        #region Device Methods

        /// <summary>
        /// Makes a POST request to the /devices Jetstream REST endpoint. If successful it returns the device included
        /// in the body of the POST request.
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
        /// Makes a DELETE request to the /devices/{deviceName} REST endpoint and removes a device from your application.
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
        /// Makes a GET request to the /devices REST endpoint, returning a list of devices and their properties.
        /// Replaces the v1.5 GetConfiguration command
        /// </summary>
        /// <returns>List of DevicesDto</returns>
        public List<DevicesDto> GetDevices(string searchName = "", string searchSerialNumber = "", string searchDeviceDefinition = "", 
            string searchRegion = "", string searchPolicy = "", string sortBy = "")
        {
            // Append the devices resource location to the url.
            var url = _baseUri + "devices?";
            url += String.IsNullOrEmpty(searchName) ? String.Empty : "name=" + searchName; // Add search string
            url += String.IsNullOrEmpty(searchSerialNumber) ? String.Empty : "&serialnumber=" + searchSerialNumber; // Add search string
            url += String.IsNullOrEmpty(searchDeviceDefinition) ? String.Empty : "&devicedefinition=" + searchDeviceDefinition; // Add search string
            url += String.IsNullOrEmpty(searchRegion) ? String.Empty : "&region=" + searchRegion; // Add search string
            url += String.IsNullOrEmpty(searchPolicy) ? String.Empty : "&policy=" + searchPolicy; // Add search string
            url += String.IsNullOrEmpty(sortBy) ? String.Empty : "&sort=" + sortBy; // Add sort string

            return SendRequestToJetstream<string, List<DevicesDto>>(url, WebRequestMethods.Http.Get, String.Empty);
        }

        /// <summary>
        /// Makes a GET request to the /devices/{name} REST endpoint, returning a specified device.
        /// </summary>
        /// <returns>DevicesDto</returns>
        public DevicesDto GetDevice(string deviceName)
        {
            // Append the devices resource location to the url.
            var url = _baseUri + "devices/" + deviceName;

            return SendRequestToJetstream<string, DevicesDto>(url, WebRequestMethods.Http.Get, String.Empty);
        }

        /// <summary>
        /// Get the device status
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>DeviceStatusDto</returns>
        public DeviceStatusDto GetDeviceStatus(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/status";

            return SendRequestToJetstream<string, DeviceStatusDto>(url, WebRequestMethods.Http.Get, String.Empty);
        }

        #endregion

        #region Device Commands

        /// <summary>
        /// Send a GetEPCListCommand to the device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendGetEpcListCommand(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/epclist";

            return SendRequestToJetstream<string, CommandResponseDto>(url, WebRequestMethods.Http.Get, String.Empty);
        }

        /// <summary>
        /// Send a ResetCommand to the device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendResetCommand(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/reset";

            return SendRequestToJetstream<string, CommandResponseDto>(url, WebRequestMethods.Http.Post, String.Empty);
        }

        /// <summary>
        /// Send an UpdateFirmwareCommand to the device
        /// to update the firmware version of the reader or agent
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
        /// Send a LockdownCommand to the device
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
        /// Send a UnlockDoorCommand to the device
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
        /// Send a GetPassesCommand to the device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendGetAccessControlCommand(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/accesscontrol";

            return SendRequestToJetstream<string, CommandResponseDto>(url, WebRequestMethods.Http.Get, String.Empty);
        }

        /// <summary>
        /// Send an UpdatePassesCommand to the device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="postAccessControlDto"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendPostAccessControlCommand(string deviceName, PostAccessControlDto postAccessControlDto)
        {
            var url = _baseUri + "devices/" + deviceName + "/accesscontrol";

            return SendRequestToJetstream<PostAccessControlDto, CommandResponseDto>(url, WebRequestMethods.Http.Post, postAccessControlDto);
        }

        /// <summary>
        /// Send an UpdatePassesCommand to the device
        /// that removes all passes on device before adding
        /// whatever new specified passes are sent.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="putAccessControlDto"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendPutAccessControlCommand(string deviceName, PutAccessControlDto putAccessControlDto)
        {
            var url = _baseUri + "devices/" + deviceName + "/accesscontrol";

            return SendRequestToJetstream<PutAccessControlDto, CommandResponseDto>(url, WebRequestMethods.Http.Put, putAccessControlDto);
        }

        /// <summary>
        /// Queues a device command to 
        /// retrieve the specified parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        public CommandResponseDto SendGetApplicationValues(List<string> parameters, string deviceName)
        {
            var joinedParameters = string.Join(",", parameters);

            // Append the resource to the url.
            var url = _baseUri + "devices/" + deviceName + "/ApplicationValues?" + joinedParameters;

            return SendRequestToJetstream<string, CommandResponseDto>(url, WebRequestMethods.Http.Get, String.Empty);
        }

        /// <summary>
        /// Queues a device command to 
        /// set the specified parameters
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
        /// Send an UpdateAppFirmwareCommand to the device
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
        /// Creates a POST request to the /devices/{deviceName}/policy
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
        /// Creates a GET request to the /devices/{deviceName}/policy
        /// endpoint to get the device's policy, if there is one.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>DevicesPolicyDto</returns>
        public DevicesPolicyDto GetDevicePolicy(string deviceName)
        {
            // Append the devices resource location to the url.
            var url = _baseUri + "devices/" + deviceName + "/policy";

            return SendRequestToJetstream<string, DevicesPolicyDto>(url, WebRequestMethods.Http.Get, String.Empty);
        }

        /// <summary>
        /// Sends a DELETE request to the /devices/{deviceName}/policy
        /// REST endpoint to remove an existing device from a policy
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
        /// Sends the current device policy, if there is one,
        /// parameters to the device.
        /// Replaces SetConfigValuesCommand
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SyncDevicePolicy(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/policy/sync";
            return SendRequestToJetstream<string, CommandResponseDto>(url, WebRequestMethods.Http.Post, String.Empty);
        }

        /// <summary>
        /// Get the currently synced device policy, if there is one.
        /// Replaces GetConfigValuesCommand
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto GetSyncedDevicePolicy(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/policy/sync";
            return SendRequestToJetstream<string, CommandResponseDto>(url, WebRequestMethods.Http.Get, String.Empty);
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
            if (!String.Equals(httpVerb, WebRequestMethods.Http.Get))
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
                // ReSharper disable once AssignNullToNotNullAttribute
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
            var formattedMessage = String.Empty;
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
                // ReSharper disable once EmptyGeneralCatchClause
                catch(Exception){}
            }
            

            // Grab the message from the body, if it exists
            if (responseObject["Message"] != null)
            {
                formattedMessage += responseObject["Message"].ToString();
            }

            // Grab and aggregate the model state errors, if they exist
            if (responseObject["ModelState"] != null)
            {
                if (!String.IsNullOrEmpty(formattedMessage))
                {
                    formattedMessage += " | ";
                }

                // Take the ModelState section of the JSON and convert it to a JToken.
                var modelStateJToken = responseObject["ModelState"];
                // Take the JToken and convert it to a Dictionary<string, List<string>>
                var modelStateDictionary = modelStateJToken.ToObject<Dictionary<string, List<string>>>();
                // Go through each error and joing them onto the string with a comma between them
                formattedMessage += String.Join(", ", modelStateDictionary.Values.Select(x => String.Join(", ", x)));
            }

            // Return the message
            return formattedMessage;
        }

        #endregion
    }
}
