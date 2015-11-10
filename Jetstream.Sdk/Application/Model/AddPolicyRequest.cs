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
    /// AddPolicy adds a policy to Jetstream for managing your device's
    /// parameters. You can define a policy for a device definition and 
    /// assign a device to the policy. Whenever an assigned device's 
    /// parameters do not match the policy a LogEntryEvent of 
    /// type PolicyException is published. 
    ///
    /// Request object for Jetstream AddPolicy
    /// </summary>
    /// <remarks></remarks>
    public class AddPolicyRequest : JetstreamRequest
    {
        private const string _addPolicy = "v1.5/application/?action=addpolicy&accesskey={0}&devicedefinitionid={1}&name={2}{3}";

        /// <summary>
        /// Default ctor
        /// </summary>
        public AddPolicyRequest()
        {
            Parameters = new List<Tuple<string, string>>();
        }

        /// <summary>
        /// The unique identifier for the device definition you want to create a policy for
        /// </summary>
        public string DeviceDefinitionId { get; set; }

        /// <summary>
        /// The DeviceDefinition parameters to set a policy value for
        /// Item1 = Parameter Name
        /// Item2 = Parameter Value
        /// </summary>
        public List<Tuple<string, string>> Parameters { get; set; }

        /// <summary>
        /// A friendly name for the policy.  This is an ASCII 255 byte string
        /// </summary>
        public string Name { get; set; }

        internal override string BuildUri(string baseUri, string accesskey)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Tuple<string, string> t in Parameters)
            {
                sb.Append("&");
                sb.Append(HttpUtility.UrlEncode(t.Item1));
                sb.Append("=");
                sb.Append(HttpUtility.UrlEncode(t.Item2));
            }
            return String.Concat(baseUri,  String.Format(_addPolicy, accesskey, DeviceDefinitionId, Name, sb));
        }
    }
}
