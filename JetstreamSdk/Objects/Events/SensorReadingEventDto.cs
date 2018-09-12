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

using System;
using System.Collections.Generic;

namespace TersoSolutions.Jetstream.SDK.Objects.Events
{
    /// <summary>
    /// The Sensor Reading Event published when a sensor has provided Jetstream with a new reading or a batch of readings
    /// </summary>
    public class SensorReadingEventDto : DeviceEventDto
    {
        /// <summary>
        /// The list of sensor readings
        /// </summary>
        public IList<SensorReadingDto> SensorReadings { get; set; }
    }

    /// <summary>
    /// A Sensor Reading that is provided to the Sensor Reading Event for publishing
    /// </summary>
    public class SensorReadingDto
    {
        /// <summary>
        /// The name of the sensor reading
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The unit of measure of the sensor reading
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The time when the sensor reading was made
        /// </summary>
        public DateTime ReadingTime { get; set; }
    }
}
