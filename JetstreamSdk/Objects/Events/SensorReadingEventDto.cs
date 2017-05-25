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
}
