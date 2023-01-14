/*
    Copyright 2023 Terso Solutions, Inc.

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
using TersoSolutions.Jetstream.Sdk.Objects.Events;

namespace TersoSolutions.Jetstream.Sdk.Objects
{
    /// <summary>
    /// A data transfer object containing properties for a set of Jetstream events.
    /// </summary>
    public class EventsDto
    {
        /// <summary>
        /// The batch id for the list of events
        /// </summary>
        public string BatchId { get; set; }

        /// <summary>
        /// The count of events in the DTO
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The list of events
        /// </summary>
        public List<EventDto> Events { get; set; }
    }
}
