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
using System.Web;

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// GetConfigValuesCommand instructs the device to return its current configuration values. 
    /// 
    /// This is the request in a RESTful pattern.
    /// </summary>
    /// <remarks></remarks>
    public class GetConfigValuesCommandRequest : JetstreamRequest
    {
        private const string _getConfigValuesCommand = "v1.5/application/?action=getconfigvaluescommand&accesskey={0}&logicaldeviceid={1}&parameters={2}";
        
        /// <summary>
        /// Default ctor
        /// </summary>
        public GetConfigValuesCommandRequest()
        {
            Parameters = new List<string>();
        }

        /// <summary>
        /// The LogicalDeviceId you want to get parameters for
        /// </summary>
        public string LogicalDeviceId { get; set; }

        /// <summary>
        /// The parameters you want to get
        /// </summary>
        public List<string> Parameters { get; set; }

        internal override string BuildUri(string baseUri, string accesskey)
        {
            // build the uri
            return String.Concat(baseUri, String.Format(_getConfigValuesCommand,
                new object[] 
                    { 
                        accesskey, 
                        HttpUtility.UrlEncode(LogicalDeviceId),
                        String.Join("_", Parameters.ToArray())
                    }));

        }
    }
}
