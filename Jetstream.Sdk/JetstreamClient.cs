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
        /// Destructor
        /// </summary>
        public void Dispose()
        {
            _optionsChangeToken?.Dispose();
        }

        #endregion

        #region Events Methods

        /// <inheritdoc />
        public EventsDto GetEvents(int limit = 100, string searchDevice = "", string searchType = "", string sortBy = "")
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

            return SendRequestToJetstream<string, EventsDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public void DeleteEvents(DeleteEventsDto deleteEventsDto)
        {
            // Append the events resource location to the url.
            const string requestPath = "events";

            // Make call. If no error thrown, we assume successful
            SendRequestToJetstream<DeleteEventsDto, string>(requestPath, "DELETE", deleteEventsDto);
        }

        #endregion

        #region Device Definition Methods

        /// <inheritdoc />
        public List<DeviceDefinitionsDto> GetDeviceDefinitions(string searchId = "", string searchName = "", string searchFirmwareVersion = "", string searchGetConfigValuesCommand = "",
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

            return SendRequestToJetstream<string, List<DeviceDefinitionsDto>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public DeviceDefinitionsDto GetDeviceDefinition(string deviceDefinitionName)
        {
            // Add the device definitions resource location to the url.
            var requestPath = "devicedefinitions/" + deviceDefinitionName;

            return SendRequestToJetstream<string, DeviceDefinitionsDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public DeviceDefinitionsDto GetNewDeviceDefinitionId(string oldId)
        {
            var requestPath = "devicedefinitions/OldId/" + oldId;

            return SendRequestToJetstream<string, DeviceDefinitionsDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Region Methods

        /// <inheritdoc />
        public List<string> GetRegions()
        {
            // Append the regions resource location to the url.
            const string requestPath = "regions";

            return SendRequestToJetstream<string, List<string>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Policy Methods

        /// <inheritdoc />
        public PoliciesDto AddPolicy(PoliciesDto policy)
        {
            // Append the policies resource location to the url.
            const string requestPath = "policies";

            return SendRequestToJetstream<PoliciesDto, PoliciesDto>(requestPath, WebRequestMethods.Http.Post, policy);
        }

        /// <inheritdoc />
        public List<PoliciesDto> GetPolicies(string searchId = "", string searchName = "", string searchDeviceDefinition = "", string sortBy = "")
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

            return SendRequestToJetstream<string, List<PoliciesDto>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public PoliciesDto GetPolicy(string policyName)
        {
            // Add the policies resource location to the url.
            var requestPath = "policies/" + policyName;

            return SendRequestToJetstream<string, PoliciesDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public void DeletePolicy(string policyName)
        {
            // Append the policies resource location to the url.
            var requestPath = "policies/" + policyName;

            // Make call. If no error thrown, we assume successful
            SendRequestToJetstream<string, string>(requestPath, "DELETE", "");
        }

        /// <inheritdoc />
        public PoliciesDto UpdatePolicy(PoliciesDto policy, string policyName)
        {
            // Append the policies resource location to the url.
            var requestPath = "policies/" + policyName;

            return SendRequestToJetstream<PoliciesDto, PoliciesDto>(requestPath, WebRequestMethods.Http.Put, policy);
        }

        #endregion

        #region Alias Methods

        /// <inheritdoc />
        public AliasDto AddAlias(AliasDto alias)
        {
            // Append the alias resource location to the url.
            const string requestPath = "aliases";

            return SendRequestToJetstream<AliasDto, AliasDto>(requestPath, WebRequestMethods.Http.Post, alias);
        }

        /// <inheritdoc />
        public AliasDto ModifyAlias(string aliasName, AliasDto aliasWithNewValues)
        {
            // Append the alias resource location to the url.
            var requestPath = "aliases/" + aliasName;

            return SendRequestToJetstream<AliasDto, AliasDto>(requestPath, WebRequestMethods.Http.Put, aliasWithNewValues);
        }

        /// <inheritdoc />
        public List<AliasDto> GetAliases(string searchId = "", string searchName = "", string searchRegion = "", string sortBy = "")
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

            return SendRequestToJetstream<string, List<AliasDto>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public AliasDto GetAlias(string aliasName)
        {
            // Append the alias resource location to the url.
            var requestPath = "aliases/" + aliasName;

            return SendRequestToJetstream<string, AliasDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public List<string> GetAliasNames()
        {
            // Append the alias resource location to the url.
            const string requestPath = "aliases/property/names";

            return SendRequestToJetstream<string, List<string>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public void DeleteAlias(string aliasName)
        {
            // Append the alias resource location to the url.
            var requestPath = "aliases/" + aliasName;

            SendRequestToJetstream<string, string>(requestPath, "DELETE", string.Empty);
        }

        #endregion

        #region Device Methods

        /// <inheritdoc />
        public DevicesDto AddDevice(DevicesDto device)
        {
            // Append the devices resource location to the url.
            const string requestPath = "devices/";

            return SendRequestToJetstream<DevicesDto, DevicesDto>(requestPath, WebRequestMethods.Http.Post, device);
        }

        /// <inheritdoc />
        public void DeleteDevice(string deviceName)
        {
            // Append the devices resource location to the url.
            var requestPath = "devices/" + deviceName;

            // Make call. If no error thrown, we assume successful
            SendRequestToJetstream<string, string>(requestPath, "DELETE", "");
        }

        /// <inheritdoc />
        public DevicesDto ModifyDevice(string deviceName, DevicesDto device)
        {
            // Append the devices resource location to the url.
            var requestPath = "devices/" + deviceName;

            return SendRequestToJetstream<DevicesDto, DevicesDto>(requestPath, WebRequestMethods.Http.Put, device);
        }

        /// <inheritdoc />
        public List<DevicesDto> GetDevices(string searchId = "", string searchName = "", string searchSerialNumber = "", string searchDeviceDefinition = "",
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
                {"sort", sortBy}
            };

            // Adds query string if there is one to the url
            requestPath += BuildQueryString(queryParams);

            return SendRequestToJetstream<string, List<DevicesDto>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public DevicesDto GetDevice(string deviceName)
        {
            // Append the devices resource location to the url.
            var requestPath = "devices/" + deviceName;

            return SendRequestToJetstream<string, DevicesDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public DeviceStatusDto GetDeviceStatus(string deviceName)
        {
            var requestPath = "devices/" + deviceName + "/status";

            return SendRequestToJetstream<string, DeviceStatusDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Device Commands

        /// <inheritdoc />
        public CommandResponseDto SendGetEpcListCommand(string deviceName)
        {
            var requestPath = "devices/" + deviceName + "/epclist";

            return SendRequestToJetstream<string, CommandResponseDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public CommandResponseDto SendResetCommand(string deviceName)
        {
            var requestPath = "devices/" + deviceName + "/reset";

            return SendRequestToJetstream<string, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, string.Empty);
        }

        /// <inheritdoc />
        public CommandResponseDto SendVersionCommand(string deviceName, VersionDto versionDto)
        {
            var requestPath = "devices/" + deviceName + "/version";

            return SendRequestToJetstream<VersionDto, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, versionDto);
        }

        /// <inheritdoc />
        public CommandResponseDto SendLockdownCommand(string deviceName, LockdownDto lockdownDto)
        {
            var requestPath = "devices/" + deviceName + "/lockdown";

            return SendRequestToJetstream<LockdownDto, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, lockdownDto);
        }

        /// <inheritdoc />
        public CommandResponseDto SendUnlockDoorCommand(string deviceName, UnlockDoorDto unlockDoorDto)
        {
            var requestPath = "devices/" + deviceName + "/unlockdoor";

            return SendRequestToJetstream<UnlockDoorDto, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, unlockDoorDto);
        }

        /// <inheritdoc />
        public CommandResponseDto SendGetApplicationValues(List<string> parameters, string deviceName)
        {
            var joinedParameters = string.Join(",", parameters);

            // Append the resource to the url.
            var requestPath = "devices/" + deviceName + "/applicationvalues?parameters=" + joinedParameters;

            return SendRequestToJetstream<string, CommandResponseDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public CommandResponseDto SendSetApplicationValues(AppConfigValuesCommandDto appConfigValuesDto, string deviceName)
        {
            // Append the resource to the url.
            var url = "devices/" + deviceName + "/applicationvalues";

            return SendRequestToJetstream<AppConfigValuesCommandDto, CommandResponseDto>(url, WebRequestMethods.Http.Post, appConfigValuesDto);
        }

        /// <inheritdoc />
        public CommandResponseDto SendUpdateApplicationVersion(ApplicationVersionDto appVersion, string deviceName)
        {
            // Append the resource to the url.
            var requestPath = "devices/" + deviceName + "/applicationversion";

            return SendRequestToJetstream<ApplicationVersionDto, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, appVersion);
        }

        #endregion

        #region Device Policy

        /// <inheritdoc />
        public DevicesPolicyDto AddDeviceToPolicy(string deviceName, DevicesPolicyDto devicesPolicy)
        {
            // Append the devices resource location to the url.
            var requestPath = "devices/" + deviceName + "/policy";

            return SendRequestToJetstream<DevicesPolicyDto, DevicesPolicyDto>(requestPath, WebRequestMethods.Http.Post, devicesPolicy);
        }

        /// <inheritdoc />
        public DevicesPolicyDto GetDevicePolicy(string deviceName)
        {
            // Append the devices resource location to the url.
            var requestPath = "devices/" + deviceName + "/policy";

            return SendRequestToJetstream<string, DevicesPolicyDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public void RemoveDeviceFromPolicy(string deviceName)
        {
            // Append the devices resource location to the url.
            var requestPath = "devices/" + deviceName + "/policy";

            // Make call and if no error is thrown we assume success
            SendRequestToJetstream<string, string>(requestPath, "DELETE", "");
        }

        /// <inheritdoc />
        public CommandResponseDto SyncDevicePolicy(string deviceName)
        {
            var requestPath = "devices/" + deviceName + "/policy/sync";
            return SendRequestToJetstream<string, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, string.Empty);
        }

        /// <inheritdoc />
        public CommandResponseDto GetSyncedDevicePolicy(string deviceName)
        {
            var requestPath = "devices/" + deviceName + "/policy/sync";
            return SendRequestToJetstream<string, CommandResponseDto>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        #endregion

        #region Device Credentials Commands

        /// <inheritdoc />
        public List<string> AddDeviceCredentials(string deviceName, DeviceCredentialTypes type, List<string> credentials)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(DeviceCredentialTypes), type);

            return SendRequestToJetstream<List<string>, List<string>>(requestPath, WebRequestMethods.Http.Post, credentials);
        }

        /// <inheritdoc />
        public List<string> AddDeviceCredentials(string deviceName, DeviceCredentialTypes type, Dictionary<string, string> credentials)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(DeviceCredentialTypes), type);

            return SendRequestToJetstream<Dictionary<string, string>, List<string>>(requestPath, WebRequestMethods.Http.Post, credentials);
        }

        /// <inheritdoc />
        public List<string> ModifyDeviceCredentials(string deviceName, DeviceCredentialTypes type, List<string> credentials)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(DeviceCredentialTypes), type);

            return SendRequestToJetstream<List<string>, List<string>>(requestPath, WebRequestMethods.Http.Put, credentials);
        }

        /// <inheritdoc />
        public List<string> ModifyDeviceCredentials(string deviceName, DeviceCredentialTypes type, Dictionary<string, string> credentials)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(DeviceCredentialTypes), type);

            return SendRequestToJetstream<Dictionary<string, string>, List<string>>(requestPath, WebRequestMethods.Http.Put, credentials);
        }

        /// <inheritdoc />
        public List<string> GetDeviceCredentials(string deviceName, DeviceCredentialTypes type)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(DeviceCredentialTypes), type);

            return SendRequestToJetstream<string, List<string>>(requestPath, WebRequestMethods.Http.Get, string.Empty);
        }

        /// <inheritdoc />
        public List<string> DeleteDeviceCredentials(string deviceName, DeviceCredentialTypes type, List<string> credentials)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/" + Enum.GetName(typeof(DeviceCredentialTypes), type);

            return SendRequestToJetstream<List<string>, List<string>>(requestPath, "DELETE", credentials);
        }

        /// <inheritdoc />
        public CommandResponseDto SyncDeviceCredentials(string deviceName)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/sync";

            return SendRequestToJetstream<string, CommandResponseDto>(requestPath, WebRequestMethods.Http.Post, string.Empty);
        }

        /// <inheritdoc />
        public DateTime? GetLastDeviceCredentialSyncTime(string deviceName)
        {
            // Append the device credential resource location to the url.
            var requestPath = "devices/" + deviceName + "/credentials/sync";

            return SendRequestToJetstream<string, DateTime?>(requestPath, WebRequestMethods.Http.Get, string.Empty);
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
        private T2 SendRequestToJetstream<T1, T2>(string requestPath, string httpVerb, T1 body)
        {
            // Set security to TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Setup url
            var url = new Uri(_baseUri + JetstreamVersion + "/" + requestPath);

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
                request = CreateRequestBody(request, JsonConvert.SerializeObject(body));
            }

            try
            {
                // Make call and get response
                var response = request.GetResponse();
                // Get response stream
                var responseStreamReader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException("Response stream is null"));
                // Read response stream
                var responseString = responseStreamReader.ReadToEnd();

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
                var responseBody = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException("Response stream is null", e)).ReadToEnd();
                // Format the JSON body of the response to make a pretty error message
                var formattedMessage = CreateErrorMessageFromJsonBody(responseBody);
                // Create new Jetstream exception and throw it
                var jetstreamException = new JetstreamException(statusCode, JsonConvert.SerializeObject(body), responseBody, formattedMessage, e);
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
            return "?" + string.Join("&", listOfParams);
        }

        #endregion

    }
}
