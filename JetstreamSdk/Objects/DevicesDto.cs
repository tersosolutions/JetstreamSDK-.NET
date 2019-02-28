﻿/*
    Copyright 2019 Terso Solutions, Inc.

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

namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// A data transfer object containing properties for a device
    /// </summary>
    public class DevicesDto
    {
        /// <summary>
        /// Autogenerated ID from the device
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// The name of the device
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The serial number of the device
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// The name of the device definition of the device
        /// </summary>
        public string DeviceDefinition { get; set; }

        /// <summary>
        /// The region of the device
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// The name of the policy assigned to the device
        /// </summary>
        public string Policy { get; set; }

        /// <summary>
        /// List of alias names the the device
        /// will be added to on creation
        /// </summary>
        public List<string> Aliases { get; set; }
    }
}
