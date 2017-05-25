namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// DTO telling the device to update the version
    /// of a specified component. Acts as a firmware upgrade.
    /// </summary>
    public class VersionDto
    {
        /// <summary>
        /// The new device definition for the device (optional)
        /// </summary>
        public string NewDeviceDefinition { get; set; }

        /// <summary>
        /// The URL at which the software update is available
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Component on the device you are updating
        /// </summary>
        public string Component { get; set; }

    }
}
