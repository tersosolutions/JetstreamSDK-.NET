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
    /// DeviceSpecificCommand instructs the device to execute one of its custom commands. 
    /// A DeviceDefinition may have custom commands that are only applicable to it. 
    /// 
    /// This class creates the request.
    /// </summary>
    /// <remarks></remarks>
    public abstract class DeviceSpecificCommandRequest : JetstreamRequest
    {
        private const string _deviceSpecificCommand = "v1.5/application/?action=devicespecificcommand&accesskey={0}&commandname={1}&logicaldeviceid={2}{3}";
        
        /// <summary>
        /// The LogicalDeviceId to queue the DeviceSpecificCommand for
        /// </summary>
        public string LogicalDeviceId { get; set; }

        /// <summary>
        /// The commandName
        /// </summary>
        public abstract string CommandName { get; }

        /// <summary>
        /// abstract strategy design patter for creating the device specific command parameters for the url
        /// </summary>
        /// <returns></returns>
        public abstract string CreateParametersStrategy();

        /// <summary>
        /// Implemented a 
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="accesskey"></param>
        /// <returns></returns>
        internal override string BuildUri(string baseUri, string accesskey)
        {
            return String.Concat(baseUri, String.Format(_deviceSpecificCommand,
                accesskey, CommandName, HttpUtility.UrlEncode(LogicalDeviceId), CreateParametersStrategy()));
        }
    }
}
