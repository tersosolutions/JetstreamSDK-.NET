﻿/*
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

using System;
using System.Collections.Generic;

namespace TersoSolutions.Jetstream.SDK.Objects.Events
{
    /// <summary>
    /// DTO for log entry events sent by device
    /// </summary>
    public class LogEntryEventDto : DeviceEventDto
    {
        /// <summary>
        /// List of log entries inside the event
        /// </summary>
        public IList<LogEntryDto> LogEntries { get; set; }
    }

    /// <summary>
    /// Custom class for use inside the log entry event class
    /// </summary>
    public class LogEntryDto
    {
        /// <summary>
        /// Level of event that occurred (Warning or Error as examples)
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// Message recorded to device's log
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Type of event
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Time of logging
        /// </summary>
        public DateTime LogTime { get; set; }
    }
}
