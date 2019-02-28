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

namespace TersoSolutions.Jetstream.SDK.Objects.Events
{
    /// <summary>
    /// The Aggregate Event is an event that is generated by a device.
    /// An Aggregate Event contains a list of EPCs
    /// that were added and removed and who did it.
    /// </summary>
    public class AggregateEventDto : DeviceEventDto
    {
        /// <summary>
        /// Pass associated with transaction
        /// </summary>
        public string PassRfid { get; set; }

        /// <summary>
        /// EPCs added in this transaction
        /// </summary>
        public IList<string> Adds { get; set; }

        /// <summary>
        /// EPCs removed in this transaction
        /// </summary>
        public IList<string> Removes { get; set; }
    }
}