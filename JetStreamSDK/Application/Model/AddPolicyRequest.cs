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
    /// AddPolicy adds a policy to Jetstream® for managing your device's
    /// parameters. You can define a policy for a device definition and 
    /// assign a device to the policy. Whenever an assigned device's 
    /// parameters do not match the policy a LogEntryEvent of 
    /// type PolicyException is published. 
    ///
    /// Request object for Jetstream version 1.1 AddPolicy
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public class AddPolicyRequest : JetstreamRequest
    {
        private const String c_addpolicy = "v1.1/application/?action=addpolicy&accesskey={0}&devicedefinitionid={1}&name={2}{3}";

        /// <summary>
        /// Default ctor
        /// </summary>
        public AddPolicyRequest()
        {
            this.Parameters = new List<Tuple<string, string>>();
        }

        /// <summary>
        /// The unique identifier for the device definition you want to create a policy for
        /// </summary>
        public String DeviceDefinitionId { get; set; }

        /// <summary>
        /// The DeviceDefinition parameters to set a policy value for
        /// Item1 = Parameter Name
        /// Item2 = Parameter Value
        /// </summary>
        public List<Tuple<String, String>> Parameters { get; set; }

        /// <summary>
        /// A friendly name for the policy.  This is an ASCII 255 byte string
        /// </summary>
        public String Name { get; set; }

        internal override string BuildUri(string baseUri, string accesskey)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Parameters.Count; i++)
            {
                sb.Append("&");
                sb.Append(HttpUtility.UrlEncode(this.Parameters[i].Item1));
                sb.Append("=");
                sb.Append(HttpUtility.UrlEncode(this.Parameters[i].Item2));
            }
            return String.Concat(baseUri,  String.Format(c_addpolicy, new Object[] 
                { 
                    accesskey, 
                    this.DeviceDefinitionId, 
                    this.Name, 
                    sb.ToString() 
                }));
        }
    }
}
