/*
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
    /// A data transfer object containing properties for an applications configuration values.
    /// </summary>
    public class AppConfigValuesCommandDto
    {
        /// <summary>
        /// A list of parameters and 
        /// the values that they are to be set to.
        /// </summary>
        public List<KeyValuePair<string, string>> Parameters { get; set; }
    }
}
