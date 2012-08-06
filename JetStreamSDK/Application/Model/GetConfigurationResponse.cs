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
using CR = TersoSolutions.Jetstream.Application.Model.Deserialized.GetConfigurationResponse;

namespace TersoSolutions.Jetstream.Application.Model
{
    /// <summary>
    /// Response object for the Jetstream version 1.1 GetConfiguration endpoint
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public class GetConfigurationResponse : JetstreamResponse
    {
        private CR.Jetstream _deserializedResponse = null;

        /// <summary>
        /// List of all LogicalDevices currently added for the application
        /// </summary>
        public List<CR.JetstreamGetConfigurationResponseLogicalDevice> LogicalDeviceList
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Body))
                {
                    _deserializedResponse = _deserializedResponse ?? CR.Jetstream.Deserialize(this.Body);
                    return _deserializedResponse.GetConfigurationResponse.LogicalDeviceList;
                }
                else
                {
                    return new List<CR.JetstreamGetConfigurationResponseLogicalDevice>();
                }
            }
        }
    }
}
