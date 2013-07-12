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
using System.Web;

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// GetConfigValuesCommand instructs the device to return its current configuration values. 
    /// 
    /// This is the request in a RESTful pattern.
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public class GetConfigValuesCommandRequest : JetstreamRequest
    {
        private const String c_getConfigValuesCommand = "v1.0/application/?action=getconfigvaluescommand&accesskey={0}&logicaldeviceid={1}&parameters={2}";
        
        /// <summary>
        /// Default ctor
        /// </summary>
        public GetConfigValuesCommandRequest()
        {
            this.Parameters = new List<String>();
        }

        /// <summary>
        /// The LogicalDeviceId you want to get parameters for
        /// </summary>
        public String LogicalDeviceId { get; set; }

        /// <summary>
        /// The parameters you want to get
        /// </summary>
        public List<String> Parameters { get; set; }

        internal override string BuildUri(string baseUri, string accesskey)
        {
            // build the uri
            return String.Concat(baseUri, String.Format(c_getConfigValuesCommand,
                new String[] 
                    { 
                        accesskey, 
                        HttpUtility.UrlEncode(this.LogicalDeviceId),
                        String.Join("_", this.Parameters.ToArray())
                    }));

        }
    }
}
