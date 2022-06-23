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

namespace TersoSolutions.Jetstream.Sdk.Objects
{
    /// <summary>
    /// A data transfer object containing properties for a command that has been sent to a device.
    /// </summary>
    public class CommandResponseDto
    {
        /// <summary>
        /// The ID for the command that is send to the device
        /// </summary>
        public string CommandId { get; set; }

        /// <summary>
        /// Indicates whether a command is queued or processed when submitted
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// A list of exceptions that occurred during the execution of a command
        /// </summary>
        public IList<KeyValuePair<string, string>> ExceptionList { get; set; }

        /// <summary>
        /// Parameter List passed back from the execution of a Jetstream command
        /// </summary>
        public IList<KeyValuePair<string, string>> OutputParameterList { get; set; }
    }
}
