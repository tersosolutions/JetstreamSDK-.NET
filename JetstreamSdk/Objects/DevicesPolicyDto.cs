using System.Collections.Generic;

namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// Summarizes data to be communicated about
    /// a device policy
    /// </summary>
    public class DevicesPolicyDto
    {
        /// <summary>
        /// The name of a DevicesPolicyDto
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parameters are settings for a particular device. Their
        /// keys must belong to a device definition, the keys
        /// are values to the setting for the device.
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }
    }
}
