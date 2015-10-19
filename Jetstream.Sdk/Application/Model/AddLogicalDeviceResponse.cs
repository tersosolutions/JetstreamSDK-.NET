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
using CR = TersoSolutions.Jetstream.SDK.Application.Model.Deserialized.ConfigureResponse;

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// AddLogicalDevice adds a device to Jetstream making it ready 
    /// for your commands and pushing events. AddLogicalDevice also 
    /// insulates your application from hardware maintenance and 
    /// replacements by mapping the device's serial number to your 
    /// application identifier. 
    /// 
    /// Response object for the Jetstream AddLogicalDevice ReST endpoint.
    /// </summary>
    /// <remarks></remarks>
    public class AddLogicalDeviceResponse : JetstreamResponse
    {
        private CR.Jetstream _deserializedResponse;
        
        /// <summary>
        /// The access key the device should use for access to the Jetstream device endpoints
        /// </summary>
        public String Id
        {
            get
            {
                if (!String.IsNullOrEmpty(Body))
                {
                    _deserializedResponse = _deserializedResponse ?? CR.Jetstream.Deserialize(Body);
                    return _deserializedResponse.ConfigureResponse.Id;
                }
                return null;
            }
        }
    }
}