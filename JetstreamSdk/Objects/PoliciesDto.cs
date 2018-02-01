/*
    Copyright 2018 Terso Solutions, Inc.

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
    /// A data transfer object that 
    /// defines a policy and its properties
    /// </summary>
    public class PoliciesDto
    {
        /// <summary>
        /// The name of the policy
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Referenced name of the device definition
        /// to which the policy applies
        /// </summary>
        public string DeviceDefinition { get; set; }

        /// <summary>
        /// Configuration parameters and their values
        /// as described in a device definition. 
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// A list of proprietary commands that are applicable
        /// to the device on which this policy is applied
        /// </summary>
        public List<string> ProprietaryCommands { get; set; }
    }
}
