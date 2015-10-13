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
    /// AddLogicalDevice adds a device to Jetstream making it ready 
    /// for your commands and pushing events. AddLogicalDevice also 
    /// insulates your application from hardware maintenance and 
    /// replacements by mapping the device's serial number to your 
    /// application identifier. 
    /// 
    /// Request object for the Jetstream AddLogicalDevice ReST endpoint.
    /// </summary>
    /// <remarks></remarks>
    public class AddLogicalDeviceRequest : JetstreamRequest
    {
        private const string _addLogicalDevice = "v1.5/application/?action=addlogicaldevice&accesskey={0}&deviceserialnumber={1}&logicaldeviceid={2}&devicedefinitionid={3}&region={4}";
       
        /// <summary>
        /// The physical device serial number.
        /// </summary>
        public string DeviceSerialNumber { get; set; }

        /// <summary>
        /// The logical device id to correlate to the device serial number.
        /// </summary>
        public string LogicalDeviceId { get; set; }

        /// <summary>
        /// The Id of Device definition currently available in Jetstream.
        /// </summary>
        public string DeviceDefinitionId { get; set; }

        /// <summary>
        /// The region that the RFID device is being placed.
        /// </summary>
        public Regions Region { get; set; }

        /// <summary>
        /// Builds the AddLogicalDevice request url
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="accesskey"></param>
        /// <returns></returns>
        internal override string BuildUri(string baseUri, string accesskey)
        {
            if (String.IsNullOrEmpty(baseUri)) throw new ArgumentNullException("baseUri");
            if (String.IsNullOrEmpty(accesskey)) throw new ArgumentNullException("accesskey");

            // build the uri
            return String.Concat(baseUri, String.Format(_addLogicalDevice, accesskey, HttpUtility.UrlEncode(DeviceSerialNumber),
                HttpUtility.UrlEncode(LogicalDeviceId), DeviceDefinitionId, Region));
        }
    }
}
