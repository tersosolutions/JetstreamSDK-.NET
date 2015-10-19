﻿﻿/*
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
    /// UpdateFirmwareCommand will command the unit to update either 
    /// the Agent firmware or the Reader firmware from a specified URL. 
    /// 
	/// Request object for the Jetstream UpdateFirmwareCommand REST endpoint.
    /// </summary>
    /// <remarks></remarks>
    public class UpdateFirmwareCommandRequest : JetstreamRequest
    {
        private const String _updateFirmwareCommand = "v1.5/application/?action=updatefirmwarecommand&accesskey={0}&logicaldeviceid={1}&component={2}&url={3}{4}";
        
        /// <summary>
        /// The LogicalDeviceId that you want to update firmware on
        /// </summary>
        public string LogicalDeviceId { get; set; }

        /// <summary>
        /// The url of the firmware file download
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The component that should update firwmare
        /// </summary>
        public Components Component { get; set; }

        /// <summary>
        /// The new DeviceDefinitionId that the firwmare will be on after the update
        /// </summary>
        public string NewDeviceDefinitionId { get; set; }

        internal override string BuildUri(string baseUri, string accesskey)
        {
            // build the uri
            return String.Concat(baseUri, String.Format(_updateFirmwareCommand, accesskey, 
                HttpUtility.UrlEncode(LogicalDeviceId), Component, HttpUtility.UrlEncode(Url), NewDeviceDefinitionId ?? String.Empty));
        }
    }

    /// <summary>
    /// The component to UpdateFirmware
    /// </summary>
    public enum Components
    {
        /// <summary>
        /// Reader component
        /// </summary>
        Reader,
        /// <summary>
        /// Agent component
        /// </summary>
        Agent
    }
}