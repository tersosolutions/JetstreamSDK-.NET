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

namespace TersoSolutions.Jetstream.Sdk.Objects
{
    /// <summary>
    /// A data transfer object containing properties for an application update
    /// </summary>
    public class ApplicationVersionDto
    {
        /// <summary>
        /// The URL at which the software update is available
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Username used to access the software update
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password used to access the software update
        /// </summary>
        public string Password { get; set; }
    }
}
