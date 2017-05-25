namespace TersoSolutions.Jetstream.SDK.Objects.Events
{
    /// <summary>
    /// The base DTO for device event inheritance
    /// </summary>
    public abstract class DeviceEventDto : EventDto
    {
        /// <summary>
        /// The name of the device referenced in 
        /// this event
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// Time that the event was received
        /// from the device
        /// </summary>
        public string ReceivedTime { get; set; }
    }
}
