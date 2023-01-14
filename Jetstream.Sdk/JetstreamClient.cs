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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TersoSolutions.Jetstream.Sdk.Objects;
using TersoSolutions.Jetstream.Sdk.Objects.Events;

namespace TersoSolutions.Jetstream.Sdk
{
    /// <inheritdoc />
    public class JetstreamClient : IJetstreamClient
    {
        #region Data

        private readonly IDisposable _optionsChangeToken;

        private bool _isDisposed;
        // The version of Jetstream to use
        private const string JetstreamVersion = "3";
        // The root of the Jetstream API in URL form
        private Uri _baseUri;
        // The access key to use in Jetstream calls
        private string _accessKey;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for the client. This client is the base service for accessing the Jetstream REST endpoints.
        /// </summary>
        /// <param name="accessKey">Jetstream access key</param>
        /// <param name="jetstreamUrl">The root url for the Jetstream web service
        /// <para>https://api.jetstreamrfid.com</para>
        /// <para>https://apibeta.jetstreamrfid.com</para>
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        public JetstreamClient(string accessKey, string jetstreamUrl)
        {
            // Validate input
            if (string.IsNullOrEmpty(jetstreamUrl)) throw new ArgumentNullException(nameof(jetstreamUrl));
            if (string.IsNullOrEmpty(accessKey)) throw new ArgumentNullException(nameof(accessKey));

            // Assign settings
            UpdateOptions(new JetstreamClientOptions
            {
                JetstreamUrl = new Uri(jetstreamUrl.Trim()),
                AccessKey = accessKey
            });
        }

        /// <summary>
        /// Constructor for the client. This client is the base service for accessing the Jetstream REST endpoints.
        /// </summary>
        /// <param name="accessKey">Jetstream access key</param>
        /// <param name="jetstreamUrl">The root url for the Jetstream web service</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="accessKey"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="jetstreamUrl"/> is <see langword="null"/></exception>
        public JetstreamClient(string accessKey, Uri jetstreamUrl)
        {
            // Validate input
            if (string.IsNullOrEmpty(accessKey)) throw new ArgumentNullException(nameof(accessKey));
            if (jetstreamUrl == null) throw new ArgumentNullException(nameof(jetstreamUrl));

            // Assign settings
            UpdateOptions(new JetstreamClientOptions
            {
                JetstreamUrl = jetstreamUrl,
                AccessKey = accessKey
            });
        }

        /// <summary>
        /// Constructor for client using DI
        /// </summary>
        /// <param name="options">Jetstream admin client options</param>
        /// <exception cref="T:System.ArgumentNullException">Options object cannot be null <paramref name="options"/></exception>
        public JetstreamClient(IOptionsMonitor<JetstreamClientOptions> options)
        {
            // Validate input
            if (options == null) throw new ArgumentNullException(nameof(options), "Options object cannot be null");

            // Handle updates
            _optionsChangeToken = options.OnChange(UpdateOptions);

            // Assign settings
            UpdateOptions(options.CurrentValue);
        }

        /// <summary>
        /// Handle updates to options
        /// </summary>
        /// <param name="options"></param>
        private void UpdateOptions(JetstreamClientOptions options)
        {
            _baseUri = options.JetstreamUrl;
            _accessKey = options.AccessKey;
        }

        /// <summary>
        /// Clean up internal things
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up internal things
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            _optionsChangeToken?.Dispose();

            _isDisposed = true;
        }

        #endregion

        #region Events Methods

        /// <inheritdoc />
        public Task<EventsDto> GetEventsAsync(int limit = 100, string searchDevice = "", string searchType = "", string sortBy = "")
        {
            // Append the events resource location to the url and add the maximum limit of events to return to the url
            var requestPath = "events/" + limit;

            var queryParams = new Dictionary<string, string>
            {
                {"device", searchDevice},
                {"type", searchType},
                {"sort", sortBy}
            };

            requestPath += BuildQueryString(queryParams);

            return SendRequestToJetstreamAsync<string, EventsDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task DeleteEventsAsync(DeleteEventsDto deleteEventsDto)
        {
            // Append the events resource location to the url.
            const string requestPath = "events";

            // Make call. If no error thrown, we assume successful
            return SendRequestToJetstreamAsync<DeleteEventsDto, string>(requestPath, "DELETE", deleteEventsDto);
        }

        #endregion

        #region Device Definition Methods

        /// <inheritdoc />
        public Task<List<DeviceDefinitionsDto>> GetDeviceDefinitionsAsync(string searchId = "", string searchName = "", string searchFirmwareVersion = "", string searchGetConfigValuesCommand = "",
            string searchSetConfigValuesCommand = "", string searchGetEpcListCommand = "", string searchResetCommand = "", string searchUpdateFirmwareCommand = "", string sortBy = "")
        {
            // Append the device definitions resource location to the url.
            var requestPath = "devicedefinitions";

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
            requestPath += BuildQueryString(queryParams);

            return SendRequestToJetstreamAsync<string, List<DeviceDefinitionsDto>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task<DeviceDefinitionsDto> GetDeviceDefinitionAsync(string deviceDefinitionName)
        {
            // Add the device definitions resource location to the url.
            var requestPath = "devicedefinitions/" + deviceDefinitionName;

            return SendRequestToJetstreamAsync<string, DeviceDefinitionsDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task<DeviceDefinitionsDto> GetNewDeviceDefinitionIdAsync(string oldId)
        {
            var requestPath = "devicedefinitions/OldId/" + oldId;

            return SendRequestToJetstreamAsync<string, DeviceDefinitionsDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Region Methods

        /// <inheritdoc />
        public Task<List<string>> GetRegionsAsync()
        {
            // Append the regions resource location to the url.
            const string requestPath = "regions";

            return SendRequestToJetstreamAsync<string, List<string>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Policy Methods

        /// <inheritdoc />
        public Task<PoliciesDto> AddPolicyAsync(PoliciesDto policy)
        {
            // Append the policies resource location to the url.
            const string requestPath = "policies";

            return SendRequestToJetstreamAsync<PoliciesDto, PoliciesDto>(requestPath, WebRequestMethods.Http.Post, policy);
        }

        /// <inheritdoc />
        public Task<List<PoliciesDto>> GetPoliciesAsync(string searchId = "", string searchName = "", string searchDeviceDefinition = "", string sortBy = "")
        {
            // Append the policies resource location to the url.
            var requestPath = "policies";

            // builds a dictionary to hold parameters
            var queryParams = new Dictionary<string, string>
            {
                {"id", searchId},
                {"name", searchName},
                {"devicedefinition", searchDeviceDefinition},
                {"sortby", sortBy}
            };

            // Adds query string if there is one to the url
            requestPath += BuildQueryString(queryParams);

            return SendRequestToJetstreamAsync<string, List<PoliciesDto>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task<PoliciesDto> GetPolicyAsync(string policyName)
        {
            // Add the policies resource location to the url.
            var requestPath = "policies/" + policyName;

            return SendRequestToJetstreamAsync<string, PoliciesDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task DeletePolicyAsync(string policyName)
        {
            // Append the policies resource location to the url.
            var requestPath = "policies/" + policyName;

            // Make call. If no error thrown, we assume successful
            return SendRequestToJetstreamAsync<string, string>(requestPath, "DELETE", "");
        }

        /// <inheritdoc />
        public Task<PoliciesDto> UpdatePolicyAsync(PoliciesDto policy, string policyName)
        {
            // Append the policies resource location to the url.
            var requestPath = "policies/" + policyName;

            return SendRequestToJetstreamAsync<PoliciesDto, PoliciesDto>(requestPath, WebRequestMethods.Http.Put, policy);
        }

        #endregion

        #region Alias Methods

        /// <inheritdoc />
        public Task<AliasDto> AddAliasAsync(AliasDto alias)
        {
            // Append the alias resource location to the url.
            const string requestPath = "aliases";

            return SendRequestToJetstreamAsync<AliasDto, AliasDto>(requestPath, WebRequestMethods.Http.Post, alias);
        }

        /// <inheritdoc />
        public Task<AliasDto> ModifyAliasAsync(string aliasName, AliasDto aliasWithNewValues)
        {
            // Append the alias resource location to the url.
            var requestPath = "aliases/" + aliasName;

            return SendRequestToJetstreamAsync<AliasDto, AliasDto>(requestPath, WebRequestMethods.Http.Put, aliasWithNewValues);
        }

        /// <inheritdoc />
        public Task<List<AliasDto>> GetAliasesAsync(string searchId = "", string searchName = "", string searchRegion = "", string sortBy = "")
        {
            // Append the alias resource location to the url.
            var requestPath = "aliases";

            // Builds a dictionary to hold parameters
            var queryParams = new Dictionary<string, string>
            {
                {"id", searchId},
                {"name", searchName},
                {"region", searchRegion},
                {"sort", sortBy}
            };

            // Adds query string if there is one to the url
            requestPath += BuildQueryString(queryParams);

            return SendRequestToJetstreamAsync<string, List<AliasDto>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task<AliasDto> GetAliasAsync(string aliasName)
        {
            // Append the alias resource location to the url.
            var requestPath = "aliases/" + aliasName;

            return SendRequestToJetstreamAsync<string, AliasDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task<List<string>> GetAliasNamesAsync()
        {
            // Append the alias resource location to the url.
            const string requestPath = "aliases/property/names";

            return SendRequestToJetstreamAsync<string, List<string>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task DeleteAliasAsync(string aliasName)
        {
            // Append the alias resource location to the url.
            var requestPath = "aliases/" + aliasName;

            return SendRequestToJetstreamAsync<string, string>(requestPath, "DELETE", string.Empty);
        }

        #endregion

        #region Device Methods

        /// <inheritdoc />
        public Task<DevicesDto> AddDeviceAsync(DevicesDto device)
        {
            // Append the devices resource location to the url.
            const string requestPath = "devices/";

            return SendRequestToJetstreamAsync<DevicesDto, DevicesDto>(requestPath, WebRequestMethods.Http.Post, device);
        }

        /// <inheritdoc />
        public Task DeleteDeviceAsync(string deviceName)
        {
            // Append the devices resource location to the url.
            var requestPath = "devices/" + deviceName;

            // Make call. If no error thrown, we assume successful
            return SendRequestToJetstreamAsync<string, string>(requestPath, "DELETE", "");
        }

        /// <inheritdoc />
        public Task<DevicesDto> ModifyDeviceAsync(string deviceName, DevicesDto device)
        {
            // Append the devices resource location to the url.
            var requestPath = "devices/" + deviceName;

            return SendRequestToJetstreamAsync<DevicesDto, DevicesDto>(requestPath, WebRequestMethods.Http.Put, device);
        }

        /// <inheritdoc />
        public Task<List<DevicesDto>> GetDevicesAsync(string searchId = "", string searchName = "", string searchSerialNumber = "", string searchDeviceDefinition = "",
            string searchRegion = "", string searchPolicy = "", string sortBy = "")
        {
            // Append the devices resource location to the url.
            var requestPath = "devices";

            // builds a dictionary to hold parameters
            var queryParams = new Dictionary<string, string>
            {
                {"id", searchId},
                {"name", searchName},
                {"serialnumber", searchSerialNumber},
                {"devicedefinition", searchDeviceDefinition},
                {"region", searchRegion},
                {"policy", searchPolicy},
                {"sort", sortBy}
            };

            // Adds query string if there is one to the url
            requestPath += BuildQueryString(queryParams);

            return SendRequestToJetstreamAsync<string, List<DevicesDto>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task<DevicesDto> GetDeviceAsync(string deviceName)
        {
            // Append the devices resource location to the url.
            var requestPath = "devices/" + deviceName;

            return SendRequestToJetstreamAsync<string, DevicesDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task<DeviceStatusDto> GetDeviceStatusAsync(string deviceName)
        {
            var requestPath = "devices/" + deviceName + "/status";

            return SendRequestToJetstreamAsync<string, DeviceStatusDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Device Commands

        /// <inheritdoc />
        public Task<CommandResponseDto> SendGetEpcListCommandAsync(string deviceName)
        {
            var requestPath = "devices/" + deviceName + "/epclist";

            return SendRequestToJetstreamAsync<string, CommandResponseDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task<CommandResponseDto> SendResetCommandAsync(string deviceName)
        {
            var requestPath = "devices/" + deviceName + "/reset";

            return SendRequestToJetstreamAsync<string, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, string.Empty);
        }

        /// <inheritdoc />
        public Task<CommandResponseDto> SendVersionCommandAsync(string deviceName, VersionDto versionDto)
        {
            var requestPath = "devices/" + deviceName + "/version";

            return SendRequestToJetstreamAsync<VersionDto, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, versionDto);
        }

        /// <inheritdoc />
        public Task<CommandResponseDto> SendLockdownCommandAsync(string deviceName, LockdownDto lockdownDto)
        {
            var requestPath = "devices/" + deviceName + "/lockdown";

            return SendRequestToJetstreamAsync<LockdownDto, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, lockdownDto);
        }

        /// <inheritdoc />
        public Task<CommandResponseDto> SendUnlockDoorCommandAsync(string deviceName, UnlockDoorDto unlockDoorDto)
        {
            var requestPath = "devices/" + deviceName + "/unlockdoor";

            return SendRequestToJetstreamAsync<UnlockDoorDto, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, unlockDoorDto);
        }

        /// <inheritdoc />
        public Task<CommandResponseDto> SendGetApplicationValuesAsync(List<string> parameters, string deviceName)
        {
            var joinedParameters = string.Join(",", parameters);

            // Append the resource to the url.
            var requestPath = "devices/" + deviceName + "/applicationvalues?parameters=" + joinedParameters;

            return SendRequestToJetstreamAsync<string, CommandResponseDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task<CommandResponseDto> SendSetApplicationValuesAsync(AppConfigValuesCommandDto appConfigValuesDto, string deviceName)
        {
            // Append the resource to the url.
            var url = "devices/" + deviceName + "/applicationvalues";

            return SendRequestToJetstreamAsync<AppConfigValuesCommandDto, CommandResponseDto>(url, WebRequestMethods.Http.Post, appConfigValuesDto);
        }

        /// <inheritdoc />
        public Task<CommandResponseDto> SendUpdateApplicationVersionAsync(ApplicationVersionDto appVersion, string deviceName)
        {
            // Append the resource to the url.
            var requestPath = "devices/" + deviceName + "/applicationversion";

            return SendRequestToJetstreamAsync<ApplicationVersionDto, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, appVersion);
        }

        #endregion

        #region Device Policy

        /// <inheritdoc />
        public Task<DevicesPolicyDto> AddDeviceToPolicyAsync(string deviceName, DevicesPolicyDto devicesPolicy)
        {
            // Append the devices resource location to the url.
            var requestPath = "devices/" + deviceName + "/policy";

            return SendRequestToJetstreamAsync<DevicesPolicyDto, DevicesPolicyDto>(requestPath, WebRequestMethods.Http.Post, devicesPolicy);
        }

        /// <inheritdoc />
        public Task<DevicesPolicyDto> GetDevicePolicyAsync(string deviceName)
        {
            // Append the devices resource location to the url.
            var requestPath = "devices/" + deviceName + "/policy";

            return SendRequestToJetstreamAsync<string, DevicesPolicyDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task RemoveDeviceFromPolicyAsync(string deviceName)
        {
            // Append the devices resource location to the url.
            var requestPath = "devices/" + deviceName + "/policy";

            // Make call and if no error is thrown we assume success
            return SendRequestToJetstreamAsync<string, string>(requestPath, "DELETE", "");
        }

        /// <inheritdoc />
        public Task<CommandResponseDto> SyncDevicePolicyAsync(string deviceName)
        {
            var requestPath = "devices/" + deviceName + "/policy/sync";
            return SendRequestToJetstreamAsync<string, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, string.Empty);
        }

        /// <inheritdoc />
        public Task<CommandResponseDto> GetSyncedDevicePolicyAsync(string deviceName)
        {
            var requestPath = "devices/" + deviceName + "/policy/sync";
            return SendRequestToJetstreamAsync<string, CommandResponseDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Device Credentials Commands

        /// <inheritdoc />
        public Task<List<string>> AddDeviceCredentialsAsync(string deviceName, DeviceCredentialTypes type, List<string> credentials)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(DeviceCredentialTypes), type);

            return SendRequestToJetstreamAsync<List<string>, List<string>>(requestPath, WebRequestMethods.Http.Post, credentials);
        }

        /// <inheritdoc />
        public Task<List<string>> AddDeviceCredentialsAsync(string deviceName, DeviceCredentialTypes type, Dictionary<string, string> credentials)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(DeviceCredentialTypes), type);

            return SendRequestToJetstreamAsync<Dictionary<string, string>, List<string>>(requestPath, WebRequestMethods.Http.Post, credentials);
        }

        /// <inheritdoc />
        public Task<List<string>> ModifyDeviceCredentialsAsync(string deviceName, DeviceCredentialTypes type, List<string> credentials)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(DeviceCredentialTypes), type);

            return SendRequestToJetstreamAsync<List<string>, List<string>>(requestPath, WebRequestMethods.Http.Put, credentials);
        }

        /// <inheritdoc />
        public Task<List<string>> ModifyDeviceCredentialsAsync(string deviceName, DeviceCredentialTypes type, Dictionary<string, string> credentials)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(DeviceCredentialTypes), type);

            return SendRequestToJetstreamAsync<Dictionary<string, string>, List<string>>(requestPath, WebRequestMethods.Http.Put, credentials);
        }

        /// <inheritdoc />
        public Task<List<string>> GetDeviceCredentialsAsync(string deviceName, DeviceCredentialTypes type)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(DeviceCredentialTypes), type);

            return SendRequestToJetstreamAsync<string, List<string>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public Task<List<string>> DeleteDeviceCredentialsAsync(string deviceName, DeviceCredentialTypes type, List<string> credentials)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(DeviceCredentialTypes), type);

            return SendRequestToJetstreamAsync<List<string>, List<string>>(requestPath, "DELETE", credentials);
        }

        /// <inheritdoc />
        public Task<CommandResponseDto> SyncDeviceCredentialsAsync(string deviceName)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/sync";

            return SendRequestToJetstreamAsync<string, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, string.Empty);
        }

        /// <inheritdoc />
        public Task<DateTime?> GetLastDeviceCredentialSyncTimeAsync(string deviceName)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/sync";

            return SendRequestToJetstreamAsync<string, DateTime?>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Helper method to serialize object into a request body and make a call to Jetstream
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="requestPath">URL path portion of the request</param>
        /// <param name="body">Any body to add to the request</param>
        /// <param name="httpVerb">HTTP verb to use</param>
        /// <returns>T</returns>
        private async Task<T2> SendRequestToJetstreamAsync<T1, T2>(string requestPath, string httpVerb, T1 body)
        {
            // Set security to TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Setup url
            var url = new Uri($"{_baseUri}{JetstreamVersion}/{requestPath}");

            // Create request
            var request = WebRequest.Create(url);

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
                request = await CreateRequestBodyAsync(request, JsonConvert.SerializeObject(body)).ConfigureAwait(false);
            }

            try
            {
                // Make call and get response
                var response = await request.GetResponseAsync().ConfigureAwait(false);
                // Get response stream
                var responseStreamReader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException("Response stream is null"));
                // Read response stream
                var responseString = await responseStreamReader.ReadToEndAsync().ConfigureAwait(false);

                // Close request and close stream
                responseStreamReader.Close();
                response.Close();

                // Return DTO returned from Jetstream
                return JsonConvert.DeserializeObject<T2>(responseString, new EventDtoConverter());
            }
            catch (WebException e)
            {
                // Cast as a http web response, if possible
                if (!(e.Response is HttpWebResponse httpResponse)) throw;

                // Grab the status code
                var statusCode = httpResponse.StatusCode;
                // Get the JSON of the response
                var responseStreamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException("Response stream is null", e));
                // Read response stream
                var responseString = await responseStreamReader.ReadToEndAsync().ConfigureAwait(false);

                // Close request and close stream
                responseStreamReader.Close();

                // Format the JSON body of the response to make a pretty error message
                var formattedMessage = CreateErrorMessageFromJsonBody(responseString);
                // Create new Jetstream exception and throw it
                var jetstreamException = new JetstreamException(statusCode, JsonConvert.SerializeObject(body), responseString, formattedMessage, e);
                throw jetstreamException;
            }
        }

        /// <summary>
        /// Encode the body of a request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="body"></param>
        /// <returns>WebRequest</returns>
        private static async Task<WebRequest> CreateRequestBodyAsync(WebRequest request, string body)
        {
            var postData = Encoding.Default.GetBytes(body);

            request.ContentLength = postData.Length;

            using (var s = await request.GetRequestStreamAsync().ConfigureAwait(false))
            {
                await s.WriteAsync(postData, 0, postData.Length).ConfigureAwait(false);
                await s.FlushAsync().ConfigureAwait(false);
            }
            return request;
        }

        /// <summary>
        /// Take the unformatted JSON of the response and create a prettier error message
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
        /// Builds a query string based on the parameters that are passed in. Ignores empty values.
        /// </summary>
        /// <param name="queryParams">Dictionary of parameter name and the value to be assigned to it</param>
        /// <returns>a string that represents the formatted query string</returns>
        private static string BuildQueryString(Dictionary<string, string> queryParams)
        {
            if (queryParams == null || !queryParams.Any()) return string.Empty;

            // Sets up a list of formatted query string values
            var listOfParams = (from p in queryParams
                                where !string.IsNullOrEmpty(p.Value)
                                select $"{p.Key.ToLower()}={p.Value}").ToList();

            // Returns the query string
            return listOfParams.Any() ? "?" + string.Join("&", listOfParams) : string.Empty;
        }

        #endregion

    }
}
