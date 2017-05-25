namespace TersoSolutions.Jetstream.SDK.Objects.Events
{
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
        public string ReadingTime { get; set; }
    }
}
