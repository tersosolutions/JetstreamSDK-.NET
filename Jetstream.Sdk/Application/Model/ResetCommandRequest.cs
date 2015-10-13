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
using System.Web;

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// ResetCommand instructs the device to power cycle. 
    /// Request object for the Jetstream ResetCommand REST endpoint.
    /// </summary>
    /// <remarks></remarks>
    public class ResetCommandRequest : JetstreamRequest
    {
        private const string _resetCommand = "v1.5/application/?action=resetcommand&accesskey={0}&logicaldeviceid={1}";
        
        /// <summary>
        /// The LogicalDeviceId to schedule a reset command for
        /// </summary>
        public string LogicalDeviceId { get; set; }

        internal override string BuildUri(string baseUri, string accesskey)
        {
            // build the uri
            return String.Concat(baseUri, String.Format(_resetCommand,
                accesskey, HttpUtility.UrlEncode(LogicalDeviceId)));
        }
    }
}
