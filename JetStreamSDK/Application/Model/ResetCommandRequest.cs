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
using System.Web;

namespace TersoSolutions.Jetstream.Application.Model
{
    /// <summary>
    /// ResetCommand request object
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public class ResetCommandRequest : JetstreamRequest
    {
        private const String c_resetCommand = "v1.0/application/?action=resetcommand&accesskey={0}&logicaldeviceid={1}";
        
        /// <summary>
        /// The LogicalDeviceId to schedule a reset command for
        /// </summary>
        public String LogicalDeviceId { get; set; }

        internal override string BuildUri(string baseUri, string accesskey)
        {
            // build the uri
            return String.Concat(baseUri, String.Format(c_resetCommand,
                accesskey, HttpUtility.UrlEncode(this.LogicalDeviceId)));
        }
    }
}
