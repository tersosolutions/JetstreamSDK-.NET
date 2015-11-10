﻿/*
     Copyright 2015 Terso Solutions, Inc.

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
using System.Text;
using System.Web;

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// AddDeviceToPolicy adds a device to a policy and will monitor the 
    /// device's configuration values for any discrepancies from the policy.
    /// Jetstream will automatically schedule a GetConfigValuesCommand 
    /// and compare the results with the policy and overridden device 
    /// parameters. If Jetstream determines that there is a variance then 
    /// a LogEntryEvent with a type of PolicyError will be published. 
    /// 
    /// Request object for the Jetstream AddDeviceToPolicy ReST endpoint.
    /// </summary>
    /// <remarks></remarks>
    public class AddDeviceToPolicyRequest : JetstreamRequest
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public AddDeviceToPolicyRequest()
        {
            OverrideParameters = new List<Tuple<String, String>>();
        }

        private const string _addDeviceToPolicy = "v1.5/application/?action=adddevicetopolicy&accesskey={0}&logicaldeviceid={1}&policyid={2}{3}";

        /// <summary>
        /// The logical device id to correlate to the device serial number. This value should be 127 ASCII characters or less
        /// </summary>
        public string LogicalDeviceId { get; set; }

        /// <summary>
        /// The unique identifier for the policy
        /// </summary>
        public string PolicyId { get; set; }

        /// <summary>
        /// List of parameters to override the policy setting for this device
        /// Item1 = parameter name
        /// Item2 = override value
        /// </summary>
        public List<Tuple<string, string>> OverrideParameters { get; set; }

        /// <summary>
        /// Builds the AddDeviceToPolicy request url
        /// </summary>
        /// <param name="baseUri">The base jetstream url https://jetstream.tersosolutions.com or https://jetstreambeta.tersosolutions.com</param>
        /// <param name="accesskey">The user&apos;s accesskey</param>
        /// <returns>The full url for calling the AddDeviceToPolicy ReST endpoint</returns>
        internal override string BuildUri(string baseUri, string accesskey)
        {
            if (String.IsNullOrEmpty(baseUri)) throw new ArgumentNullException("baseUri");
            if (String.IsNullOrEmpty(accesskey)) throw new ArgumentNullException("accesskey");

            // build the url for the overrideparams
            StringBuilder sb = new StringBuilder();
            if (OverrideParameters.Count > 0)
            {
                foreach (Tuple<string, string> t in OverrideParameters)
                {
                    sb.Append("&");
                    sb.Append(HttpUtility.UrlEncode(t.Item1));
                    sb.Append("=");
                    sb.Append(HttpUtility.UrlEncode(t.Item2));
                }
            }

            // build & return the url
            return String.Concat(baseUri, String.Format(_addDeviceToPolicy, accesskey, HttpUtility.UrlEncode(LogicalDeviceId), HttpUtility.UrlEncode(PolicyId), sb));
        }
    }
}
