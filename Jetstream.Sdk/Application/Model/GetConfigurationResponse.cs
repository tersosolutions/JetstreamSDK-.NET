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
using CR = TersoSolutions.Jetstream.SDK.Application.Model.Deserialized.GetConfigurationResponse;

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// GetConfiguration returns the current Jetstream configuration for your application. 
    /// 
    /// Response object for the Jetstream GetConfiguration endpoint.
    /// </summary>
    /// <remarks></remarks>
    public class GetConfigurationResponse : JetstreamResponse
    {
        private CR.Jetstream _deserializedResponse;

        /// <summary>
        /// List of all LogicalDevices currently added for the application
        /// </summary>
        public List<CR.JetstreamGetConfigurationResponseLogicalDevice> LogicalDeviceList
        {
            get
            {
                if (!String.IsNullOrEmpty(Body))
                {
                    _deserializedResponse = _deserializedResponse ?? CR.Jetstream.Deserialize(Body);
                    return _deserializedResponse.GetConfigurationResponse.LogicalDeviceList;
                }
                return new List<CR.JetstreamGetConfigurationResponseLogicalDevice>();
            }
        }
    }
}
