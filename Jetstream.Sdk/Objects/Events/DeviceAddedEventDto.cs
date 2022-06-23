﻿/*
    Copyright 2022 Terso Solutions, Inc.

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

using System.Collections.Generic;

namespace TersoSolutions.Jetstream.Sdk.Objects.Events
{
    /// <summary>
    /// The Device Added Event is generated by Jetstream when a device is added to an application.
    /// </summary>
    public class DeviceAddedEventDto : EventDto
    {
        /// <summary>
        /// Autogenerated id for the device
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// Friendly name of device added. 
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// List of Alias names
        /// </summary>
        public List<string> Aliases { get; set; }

        /// <summary>
        /// The name of the Device Definition the device is associated with
        /// </summary>
        public string DeviceDefinition { get; set; }

        /// <summary>
        /// The serial number of the device
        /// </summary>
        public string DeviceSerialNumber { get; set; }

        /// <summary>
        /// The region of the device
        /// </summary>
        public string Region { get; set; }
    }
}
