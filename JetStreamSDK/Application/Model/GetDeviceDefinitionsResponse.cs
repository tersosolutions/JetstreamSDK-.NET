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

using DD = TersoSolutions.Jetstream.SDK.Application.Model.Deserialized.GetDeviceDefinitionsResponse;

namespace TersoSolutions.Jetstream.SDK.Application.Model
{
    /// <summary>
    /// GetDeviceDefinitions returns all of the available device configurations 
    /// that Jetstream® supports. A device definition is a unique definition 
    /// for a specific device manufacturer, name of the device and 
    /// its firmware version. 
    ///
    /// Response object for Jetstream GetDeviceDefinitions.
    /// </summary>
    /// <remarks>Author Mike Lohmeier</remarks>
    public class GetDeviceDefinitionsResponse : JetstreamResponse
    {
        private DD.Jetstream _deserializedResponse = null;

        /// <summary>
        /// List of all DeviceDefinitions currently supported by Jetstream
        /// </summary>
        public List<DD.JetstreamGetDeviceDefinitionsResponseDeviceDefinition> DeviceDefinitionList
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Body))
                {
                    _deserializedResponse = _deserializedResponse ?? DD.Jetstream.Deserialize(this.Body);
                    return _deserializedResponse.GetDeviceDefinitionsResponse.DeviceDefinitionList;
                }
                else
                {
                    return new List<DD.JetstreamGetDeviceDefinitionsResponseDeviceDefinition>();
                }
            }
        }
    }
}
