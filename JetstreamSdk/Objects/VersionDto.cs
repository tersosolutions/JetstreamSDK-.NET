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

namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// DTO telling the device to update the version
    /// of a specified component. Acts as a firmware upgrade.
    /// </summary>
    public class VersionDto
    {
        /// <summary>
        /// The new device definition for the device (optional)
        /// </summary>
        public string NewDeviceDefinition { get; set; }

        /// <summary>
        /// The URL at which the software update is available
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Component on the device you are updating
        /// </summary>
        public string Component { get; set; }
    }
}
