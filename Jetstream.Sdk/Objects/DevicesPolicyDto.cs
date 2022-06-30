/*
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

using System;
using System.Collections.Generic;

namespace TersoSolutions.Jetstream.Sdk.Objects
{
    /// <summary>
    /// A data transfer object containing properties for a device's policy.
    /// </summary>
    public class DevicesPolicyDto
    {
        /// <summary>
        /// The ID of the policy
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// The name of a DevicesPolicyDto
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parameters are settings for a particular device. Their keys must belong to a device definition, the keys
        /// are values to the setting for the device.
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// Last time that device policy was updated but not synced.
        /// </summary>
        public DateTime LastPolicyUpdate { get; set; }

        /// <summary>
        /// Last time that device policy was synced.
        /// </summary>
        public DateTime LastPolicySync { get; set; }
    }
}
