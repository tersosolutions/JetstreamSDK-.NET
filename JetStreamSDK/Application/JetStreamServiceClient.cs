﻿/*
     Copyright 2012 Terso Solutions

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
using System.IO;
using System.Net;
using System.Text;

using TersoSolutions.Jetstream.Application.Model;

namespace TersoSolutions.Jetstream.Application
{
    /// <summary>
    /// Client proxy object for the Jetstream Web Service
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public class JetstreamServiceClient
    {
        #region Data

        private String _baseUri;
        private String _accessKey;

        #endregion

        #region Ctor(s)

        /// <summary>
        /// Constructor for the JetstreamServiceClient.  This client is the base service for accessing the Jetstream REST endpoints.
        /// </summary>
        /// <param name="jetstreamAPIUrl">The root https url for the Jetstream web service.
        /// <para>https://jetstream.tersosolutions.com</para>
        /// <para>https://jetstreambeta.tersosolutions.com</para>
        /// </param>
        /// <param name="accessKey">Your accesskey for Jetstream</param>
        /// <exception cref="System.ArgumentNullException">
        /// <para>JetstreamUrl is null</para>
        /// <para>userName is null</para>
        /// <para>password is null</para>
        /// </exception>
        public JetstreamServiceClient(String jetstreamAPIUrl, String accessKey)
        {
            if (String.IsNullOrEmpty(jetstreamAPIUrl)) throw new ArgumentNullException("JetstreamBaseUrl");
            if (String.IsNullOrEmpty(accessKey)) throw new ArgumentNullException("accessKey");

            // normalize the url with a trail /
            jetstreamAPIUrl = jetstreamAPIUrl.Trim();
            if (!jetstreamAPIUrl.EndsWith("/"))
            {
                jetstreamAPIUrl += "/";
            }

            _baseUri = jetstreamAPIUrl;
            _accessKey = accessKey;
        }

        #endregion

        #region Version 1.0 Methods

        /// <summary>
        /// Calls the Jetstream v1.0 AddLogicalDevice REST endpoint and adds a device to your application. 
        /// https://jetstreamrfid.com/Docs/Application/1.0/AddLogicalDevice.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public AddLogicalDeviceResponse AddLogicalDevice(AddLogicalDeviceRequest request)
        {
            return Execute<AddLogicalDeviceResponse>(request);
        }


        /// <summary>
        /// Calls the Jetstream v1.1 GetConfiguration REST endpoint
        /// https://jetstreamrfid.com/Docs/Application/1.1/GetConfiguration.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public GetConfigurationResponse GetConfiguration(GetConfigurationRequest request)
        {
            return Execute<GetConfigurationResponse>(request);
        }

        /// <summary>
        /// Calls the Jetstream v1.0 GetDeviceDefinitions REST endpoint
        /// https://jetstreamrfid.com/Docs/Application/1.0/GetDeviceDefinitions.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public GetDeviceDefinitionsResponse GetDeviceDefinitions(GetDeviceDefinitionsRequest request)
        {
            return Execute<GetDeviceDefinitionsResponse>(request);
        }

        /// <summary>
        /// Calls the Jetstream v1.0 RemoveLogicalDevice REST endpoint and removes a device from your application.
        /// https://jetstreamrfid.com/Docs/Application/1.0/RemoveLogicalDevice.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public RemoveLogicalDeviceResponse RemoveLogicalDevice(RemoveLogicalDeviceRequest request)
        {
            return Execute<RemoveLogicalDeviceResponse>(request);
        }

        /// <summary>
        /// Calls the Jetstream v1.0 GetEPCListCommand REST endpoint to queue a command
        /// https://jetstreamrfid.com/Docs/Application/1.0/GetEPCListCommand.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>       
        public GetEPCListCommandResponse GetEPCListCommand(GetEPCListCommandRequest request)
        {
            return Execute<GetEPCListCommandResponse>(request);
        }

        /// <summary>
        /// Calls the Jetstream v1.0 DeviceSpecificCommand REST endpoint to queue a command specific to an individual device type
        /// https://jetstreamrfid.com/Docs/Application/1.0/DeviceSpecificCommand.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public DeviceSpecificCommandResponse DeviceSpecificCommand(DeviceSpecificCommandRequest request)
        {
            return Execute<DeviceSpecificCommandResponse>(request);
        }

        /// <summary>
        /// Calls the Jetstream v1.0 ResetCommand REST endpoint to queue a command
        /// https://jetstreamrfid.com/Docs/Application/1.0/ResetCommand.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public ResetCommandResponse ResetCommand(ResetCommandRequest request)
        {
            return Execute<ResetCommandResponse>(request);
        }

        /// <summary>
        /// Calls the Jetstream v1.0 GetConfigValuesCommand REST endpoint to queue a command
        /// https://jetstreamrfid.com/Docs/Application/1.0/GetConfigValuesCommand.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public GetConfigValuesCommandResponse GetConfigValuesCommand(GetConfigValuesCommandRequest request)
        {
            return Execute<GetConfigValuesCommandResponse>(request);
        }

        /// <summary>
        /// Calls the Jetstream v1.0 SetConfigValuesCommand REST endpoint to queue a command
        /// https://jetstreamrfid.com/Docs/Application/1.0/SetConfigValuesCommand.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public SetConfigValuesCommandResponse SetConfigValuesCommand(SetConfigValuesCommandRequest request)
        {
            return Execute<SetConfigValuesCommandResponse>(request);
        }

        /// <summary>
        /// Calls the Jetstream v1.0 UpdateFirmwareCommand REST endpoint to queue a command
        /// https://jetstreamrfid.com/Docs/Application/1.0/UpdateFirmwareCommand.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public UpdateFirmwareCommandResponse UpdateFirmwareCommand(UpdateFirmwareCommandRequest request)
        {
            return Execute<UpdateFirmwareCommandResponse>(request);
        }

        #endregion

        #region Version 1.1 Methods

        /// <summary>
        /// Calls the Jetstream v1.1 AddDeviceToPolicy REST endpoint and adds a device to one of your application's policies.
        /// https://jetstreamrfid.com/Docs/Application/1.1/AddDeviceToPolicy.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public AddDeviceToPolicyResponse AddDeviceToPolicy(AddDeviceToPolicyRequest request)
        {
            return Execute<AddDeviceToPolicyResponse>(request);
        }

        /// <summary>
        /// Calls the Jetstream v1.1 AddPolicy REST endpoint and adds a new policy to your application.
        /// https://jetstreamrfid.com/Docs/Application/1.1/AddPolicy.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public AddPolicyResponse AddPolicy(AddPolicyRequest request)
        {
            return Execute<AddPolicyResponse>(request);
        }

        /// <summary>
        /// Calls the Jetstream v1.1 GetPolicies REST endpoint and adds a new policy to your application.
        /// https://jetstreamrfid.com/Docs/Application/1.1/GetPolicies.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public GetPoliciesResponse GetPolicies(GetPoliciesRequest request)
        {
            return Execute<GetPoliciesResponse>(request);
        }

        /// <summary>
        /// Calls the Jetstream v1.1 RemovePolicy REST endpoint and removes an existing policy from your application.
        /// https://jetstreamrfid.com/Docs/Application/1.1/RemovePolicy.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public RemovePolicyResponse RemovePolicy(RemovePolicyRequest request)
        {
            return Execute<RemovePolicyResponse>(request);
        }

        /// <summary>
        /// Calls the Jetstream v1.1 RemoveDeviceFromPolicy REST endpoint and removes an existing device from a polic
        /// https://jetstreamrfid.com/Docs/Application/1.1/RemoveDeviceFromPolicy.aspx
        /// </summary>
        /// <param name="request">Object that represents the HTTP Jetstream request</param>
        /// <returns>Object that represents the HTTP Jetstream response</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <para><paramref name="request"/> is null</para>
        /// </exception>
        public RemoveDeviceFromPolicyResponse RemoveDeviceFromPolicy(RemoveDeviceFromPolicyRequest request)
        {
            return Execute<RemoveDeviceFromPolicyResponse>(request);
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Helper method to execute the syncronous call to Jetstream
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private T Execute<T>(JetstreamRequest request) where T : JetstreamResponse, new()
        {
            if (request == null) throw new ArgumentNullException("request");

            // build the url & send the request to Jetstream
            String url = request.BuildUri(_baseUri, _accessKey);
            Object[] response = SendToJetStream(url, String.Empty);

            // build the response object
            if (((int)response[1] == 200))
            {
                T jsResponse = new T();
                jsResponse.Body = (String)response[0];
                jsResponse.Request = url;
                return jsResponse;
            }
            else
            {
                // non 200 error throw the custom exception
                throw new JetstreamResponseException(((int)response[1]), 
                    ((String)response[2]), url, ((String)response[0]));
            }
        }

        /// <summary>
        /// Helper method to send a request to Jetstream
        /// </summary>
        /// <param name="url">The url to send to</param>
        /// <param name="body">The HTTP POST body</param>
        /// <returns>
        /// [0] = HTTP Response body:string
        /// [1] = HTTP Response code:int
        /// [2] = HTTP Response code description: string
        /// </returns>
        private static object[] SendToJetStream(String url, String body)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            try
            {
                // create the new httpwebrequest with the uri for Jetstream
                request.ContentLength = 0;
                // set the method to POST
                request.Method = "POST";

                if (!String.IsNullOrEmpty(body))
                {
                    request.ContentType = "application/xml; charset=utf-8";
                    byte[] postData = Encoding.Default.GetBytes(body);
                    request.ContentLength = postData.Length;
                    using (Stream s = request.GetRequestStream())
                    {
                        s.Write(postData, 0, postData.Length);
                    }

                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    String responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    return new Object[] { responseString, ((int)response.StatusCode), response.StatusDescription };
                }
            }
            catch (WebException e)
            {
                // try to dump the response more
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = response as HttpWebResponse;
                    if (httpResponse != null)
                    {
                        using (Stream data = response.GetResponseStream())
                        {
                            data.Position = 0;
                            return new Object[] { new StreamReader(data).ReadToEnd(), ((int)httpResponse.StatusCode), httpResponse.StatusDescription };
                        }
                    }
                    else
                    {
                        return new Object[] { String.Empty, 500, "Server Error" };
                    }
                }
            }
        }

        #endregion
    }
}
