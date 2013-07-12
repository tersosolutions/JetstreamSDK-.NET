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
    /// AddLogicalDevice adds a device to Jetstream® making it ready 
    /// for your commands and pushing events. AddLogicalDevice also 
    /// insulates your application from hardware maintenance and 
    /// replacements by mapping the device's serial number to your 
    /// application identifier. 
    /// 
    /// Request object for the version 1.0 AddLogicalDevice ReST endpoint.
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public class AddLogicalDeviceRequest : JetstreamRequest
    {
        private const String c_addLogicalDevice = "v1.0/application/?action=addlogicaldevice&accesskey={0}&deviceserialnumber={1}&logicaldeviceid={2}&devicedefinitionid={3}&region={4}";
       
        /// <summary>
        /// The physical device serial number.
        /// </summary>
        public String DeviceSerialNumber { get; set; }

        /// <summary>
        /// The logical device id to correlate to the device serial number.
        /// </summary>
        public String LogicalDeviceId { get; set; }

        /// <summary>
        /// The Id of Device definition currently available in Jetstream.
        /// </summary>
        public String DeviceDefinitionId { get; set; }

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
            if (String.IsNullOrEmpty(baseUri)) throw new ArgumentNullException("baseUrl");
            if (String.IsNullOrEmpty(accesskey)) throw new ArgumentNullException("accesskey");

            // build the uri
            return String.Concat(baseUri, String.Format(c_addLogicalDevice,
                new Object[] { accesskey, HttpUtility.UrlEncode(this.DeviceSerialNumber), 
                    HttpUtility.UrlEncode(this.LogicalDeviceId), this.DeviceDefinitionId, 
                    this.Region }));
        }
    }
}
