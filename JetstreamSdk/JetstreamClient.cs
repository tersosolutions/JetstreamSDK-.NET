using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
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

            url += String.IsNullOrEmpty(searchDevice) ? String.Empty : "Device=" + searchDevice; // Add search string
            url += String.IsNullOrEmpty(searchType) ? String.Empty : "&Type=" + searchType; // Add search string
            url += String.IsNullOrEmpty(sortBy) ? String.Empty : "&Sort=" + sortBy; // Add sort string

            return SendRequestToJetstream<String, EventsDto>(url, WebRequestMethods.Http.Get, "");
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
            SendRequestToJetstream<DeleteEventsDto, String>(url, "DELETE", deleteEventsDto);
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
            var url = _baseUri + "DeviceDefinitions?";

            url += String.IsNullOrEmpty(searchName) ? String.Empty : "Name=" + searchName; // Add search string
            url += String.IsNullOrEmpty(searchFirmwareVersion) ? String.Empty : "&FirmwareVersion=" + searchFirmwareVersion; // Add search string
            url += String.IsNullOrEmpty(searchGetConfigValuesCommand) ? String.Empty : "&GetConfigValuesCommand=" + searchGetConfigValuesCommand; // Add search string
            url += String.IsNullOrEmpty(searchSetConfigValuesCommand) ? String.Empty : "&SetConfigValuesCommand=" + searchSetConfigValuesCommand; // Add search string
            url += String.IsNullOrEmpty(searchGetEpcListCommand) ? String.Empty : "&GetEpcListCommand=" + searchGetEpcListCommand; // Add search string
            url += String.IsNullOrEmpty(searchResetCommand) ? String.Empty : "&ResetCommand=" + searchResetCommand; // Add search string
            url += String.IsNullOrEmpty(searchUpdateFirmwareCommand) ? String.Empty : "&UpdateFirmwareCommand=" + searchUpdateFirmwareCommand; // Add search string
            url += String.IsNullOrEmpty(sortBy) ? String.Empty : "&Sort=" + sortBy; // Add sort string

            return SendRequestToJetstream<String, List<DeviceDefinitionsDto>>(url, WebRequestMethods.Http.Get, "");
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

            return SendRequestToJetstream<String, DeviceDefinitionsDto>(url, WebRequestMethods.Http.Get, "");
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
            var url = _baseUri + "Policies?";

            url += String.IsNullOrEmpty(searchName) ? String.Empty : "Name=" + searchName; // Add search string
            url += String.IsNullOrEmpty(searchDeviceDefinition) ? String.Empty : "&DeviceDefinition=" + searchDeviceDefinition; // Add search string
            url += String.IsNullOrEmpty(sortBy) ? String.Empty : "&SortBy=" + sortBy; // Add search string

            return SendRequestToJetstream<String, List<PoliciesDto>>(url, WebRequestMethods.Http.Get, "");
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

            return SendRequestToJetstream<String, PoliciesDto>(url, WebRequestMethods.Http.Get, "");
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
            SendRequestToJetstream<String, String>(url, "DELETE", "");
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
            SendRequestToJetstream<String, String>(url, "DELETE", "");

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
            url += String.IsNullOrEmpty(searchName) ? String.Empty : "Name=" + searchName; // Add search string
            url += String.IsNullOrEmpty(searchSerialNumber) ? String.Empty : "&SerialNumber=" + searchSerialNumber; // Add search string
            url += String.IsNullOrEmpty(searchDeviceDefinition) ? String.Empty : "&DeviceDefinition=" + searchDeviceDefinition; // Add search string
            url += String.IsNullOrEmpty(searchRegion) ? String.Empty : "&Region=" + searchRegion; // Add search string
            url += String.IsNullOrEmpty(searchPolicy) ? String.Empty : "&Policy=" + searchPolicy; // Add search string
            url += String.IsNullOrEmpty(sortBy) ? String.Empty : "&Sort=" + sortBy; // Add sort string

            return SendRequestToJetstream<String, List<DevicesDto>>(url, WebRequestMethods.Http.Get, "");
        }

        /// <summary>
        /// Makes a GET request to the /devices/{name} REST endpoint, returning a specified device.
        /// </summary>
        /// <returns>DevicesDto</returns>
        public DevicesDto GetDevice(string deviceName)
        {
            // Append the devices resource location to the url.
            var url = _baseUri + "devices/" + deviceName;

            return SendRequestToJetstream<String, DevicesDto>(url, WebRequestMethods.Http.Get, "");
        }

        /// <summary>
        /// Get the device status
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>DeviceStatusDto</returns>
        public DeviceStatusDto GetDeviceStatus(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/Status";

            return SendRequestToJetstream<String, DeviceStatusDto>(url, WebRequestMethods.Http.Get, "");
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
            var url = _baseUri + "devices/" + deviceName + "/EPCList";

            return SendRequestToJetstream<String, CommandResponseDto>(url, WebRequestMethods.Http.Get, "");
        }

        /// <summary>
        /// Send a ResetCommand to the device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendResetCommand(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/Reset";

            return SendRequestToJetstream<String, CommandResponseDto>(url, WebRequestMethods.Http.Post, "");
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
            var url = _baseUri + "devices/" + deviceName + "/Version";

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
            var url = _baseUri + "devices/" + deviceName + "/Lockdown";

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
            var url = _baseUri + "devices/" + deviceName + "/UnlockDoor";

            return SendRequestToJetstream<UnlockDoorDto, CommandResponseDto>(url, WebRequestMethods.Http.Post, unlockDoorDto);
        }

        /// <summary>
        /// Send a GetPassesCommand to the device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendGetAccessControlCommand(string deviceName)
        {
            var url = _baseUri + "devices/" + deviceName + "/AccessControl";

            return SendRequestToJetstream<String, CommandResponseDto>(url, WebRequestMethods.Http.Get, "");
        }

        /// <summary>
        /// Send an UpdatePassesCommand to the device
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="postAccessControlDto"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendPostAccessControlCommand(string deviceName, PostAccessControlDto postAccessControlDto)
        {
            var url = _baseUri + "devices/" + deviceName + "/AccessControl";

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
            var url = _baseUri + "devices/" + deviceName + "/AccessControl";

            return SendRequestToJetstream<PutAccessControlDto, CommandResponseDto>(url, WebRequestMethods.Http.Put, putAccessControlDto);
        }

        /// <summary>
        /// Queues a proprietary command, whose name matches the
        /// user specified strings in the policy, queues a 
        /// CommandQueuedEvent, and returns a command response
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="commandName"></param>
        /// <param name="commandParameters"></param>
        /// <returns>CommandResponseDto</returns>
        public CommandResponseDto SendProprietaryCommand(string deviceName, string commandName, ProprietaryCommandDto commandParameters)
        {
            // Append the resource to the url.
            var url = _baseUri + "devices/" + deviceName + "/" + commandName;

            return SendRequestToJetstream<ProprietaryCommandDto, CommandResponseDto>(url, WebRequestMethods.Http.Post, commandParameters);
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

            return SendRequestToJetstream<String, DevicesPolicyDto>(url, WebRequestMethods.Http.Get, "");
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
            SendRequestToJetstream<String, String>(url, "DELETE", "");
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
            return SendRequestToJetstream<String, CommandResponseDto>(url, WebRequestMethods.Http.Post, "");
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
            return SendRequestToJetstream<String, CommandResponseDto>(url, WebRequestMethods.Http.Get, "");
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

            // Make call and get response
            var response = request.GetResponse(); 
            // ReSharper disable once AssignNullToNotNullAttribute
            // Read response stream
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            // Return DTO returned from Jetstream
            return JsonConvert.DeserializeObject<T2>(responseString, new EventDtoConverter()); 
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

        #endregion
    }
}
