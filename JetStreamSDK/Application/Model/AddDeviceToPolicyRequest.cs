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
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace TersoSolutions.Jetstream.Application.Model
{
    /// <summary>
    /// Request object for Jetstream v1.1 AddDeviceToPolicy ReST endpoint
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public class AddDeviceToPolicyRequest : JetstreamRequest
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public AddDeviceToPolicyRequest()
        {
            this.OverrideParameters = new List<Tuple<String, String>>();
        }

        private const String c_adddevicetopolicy = "v1.1/application/?action=adddevicetopolicy&accesskey={0}&logicaldeviceid={1}&policyid={2}{3}";

        /// <summary>
        /// The logical device id to correlate to the device serial number. This value should be 127 ASCII characters or less
        /// </summary>
        public String LogicalDeviceId { get; set; }

        /// <summary>
        /// The unique identifier for the policy
        /// </summary>
        public String PolicyId { get; set; }

        /// <summary>
        /// List of parameters to override the policy setting for this device
        /// Item1 = parameter name
        /// Item2 = override value
        /// </summary>
        public List<Tuple<String, String>> OverrideParameters { get; set; }

        /// <summary>
        /// Builds the AddDeviceToPolicy request url
        /// </summary>
        /// <param name="baseUri">The base jetstream url https://jetstream.tersosolutions.com or https://jetstreambeta.tersosolutions.com</param>
        /// <param name="accesskey">The user&apos;s accesskey</param>
        /// <returns>The full url for calling the AddDeviceToPolicy ReST endpoint</returns>
        internal override string BuildUri(string baseUri, string accesskey)
        {
            if (String.IsNullOrEmpty(baseUri)) throw new ArgumentNullException("baseUrl");
            if (String.IsNullOrEmpty(accesskey)) throw new ArgumentNullException("accesskey");

            // build the url for the overrideparams
            StringBuilder sb = new StringBuilder();
            if (OverrideParameters.Count > 0)
            {
                for (int i = 0; i < OverrideParameters.Count; i++)
                {
                    sb.Append("&");
                    sb.Append(HttpUtility.UrlEncode(OverrideParameters[i].Item1));
                    sb.Append("=");
                    sb.Append(HttpUtility.UrlEncode(OverrideParameters[i].Item2));
                }
            }

            // build & return the url
            return String.Concat(baseUri, String.Format(c_adddevicetopolicy,
                new Object[] { accesskey, HttpUtility.UrlEncode(this.LogicalDeviceId), 
                    HttpUtility.UrlEncode(this.PolicyId), sb.ToString() }));
        }
    }
}
