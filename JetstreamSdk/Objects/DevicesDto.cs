namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// Defines information passed
    /// around about a Jetstream device
    /// </summary>
    public class DevicesDto
    {
        /// <summary>
        /// The name of the device
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The serial number of the device
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// The name of the device definition of the device
        /// </summary>
        public string DeviceDefinition { get; set; }

        /// <summary>
        /// The region of the device
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// The name of the policy assigned to the device
        /// </summary>
        public string Policy { get; set; }
    }
}
