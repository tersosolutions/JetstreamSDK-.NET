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
    /// SetConfigValuesCommand instructs the device to set its configuration parameters to user provided values. 
    /// Request object for the Jetstream SetConfigValues REST endpoint.
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class SetConfigValuesCommandRequest : JetstreamRequest
    {
        private const string _setConfigValuesCommand = "v1.5/application/?action=setconfigvaluescommand&accesskey={0}&logicaldeviceid={1}{2}";
       
        /// <summary>
        /// Default ctor
        /// </summary>
        public SetConfigValuesCommandRequest()
        {
            Parameters = new List<Tuple<string, string>>();
        }

        /// <summary>
        /// The LogicalDeviceId to queue a SetConfigValuesCommand for
        /// </summary>
        public string LogicalDeviceId { get; set; }

        /// <summary>
        /// The parameters to set
        /// Item1 = Name of parameter
        /// Item2 = Value of parameter
        /// </summary>
        public List<Tuple<string, string>> Parameters { get; set; }

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

            // build the uri
            return String.Concat(baseUri, String.Format(_setConfigValuesCommand,
                new object[] 
                    { 
                        accesskey, 
                        HttpUtility.UrlEncode(LogicalDeviceId),
                        sb.ToString()
                    }));
        }
    }
}
