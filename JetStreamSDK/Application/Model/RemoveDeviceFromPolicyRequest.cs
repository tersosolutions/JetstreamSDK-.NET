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

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// RemoveDeviceFromPolicy removes the device from the list of devices 
    /// to monitor for compliance with your policy. Once removed from the 
    /// policy all settings will be managed using GetConfigValuesCommand 
    /// and SetConfigValuesCommand manually. 
    ///
    /// Request object for the Jetstream version 1.1 RemoveDeviceFromPolicy endpoint.
    /// </summary>
    /// <remarks></remarks>
    public class RemoveDeviceFromPolicyRequest : JetstreamRequest
    {
        private const String c_removedevicefrompolicy = "v1.2/application/?action=removedevicefrompolicy&accesskey={0}&logicaldeviceid={1}";
       
        /// <summary>
        /// The LogicalDeviceId of the device to remove from the policy
        /// </summary>
        public String LogicalDeviceId { get; set; }


        internal override string BuildUri(string baseUri, string accesskey)
        {
            return String.Concat(baseUri, String.Format(
                c_removedevicefrompolicy,accesskey, this.LogicalDeviceId));
        }
    }
}
