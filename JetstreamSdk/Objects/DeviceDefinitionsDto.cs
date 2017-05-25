using System.Collections.Generic;

namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// A data transfer object that includes
    /// properties relevant to the 
    /// defining a device.
    /// </summary>
    public class DeviceDefinitionsDto
    {
        /// <summary>
        /// The name given to the device definition.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The firmware version associated with this device definition.
        /// </summary>
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// GetConfigValuesCommand supported?
        /// </summary>
        public bool GetConfigValuesCommand { get; set; }

        /// <summary>
        /// SetConfigValuesCommand supported?
        /// </summary>
        public bool SetConfigValuesCommand { get; set; }
        
        /// <summary>
        /// GetEpcListCommand supported?
        /// </summary>
        public bool GetEpcListCommand { get; set; }

        /// <summary>
        /// ResetCommand supported?
        /// </summary>
        public bool ResetCommand { get; set; }

        /// <summary>
        /// UpdateFirmwareCommand supported?
        /// </summary>
        public bool UpdateFirmwareCommand { get; set; }

        /// <summary>
        /// List of DeviceSpecificCommands that the device supports.
        /// </summary>
        public List<string> DeviceSpecificCommandNames { get; set; }

        /// <summary>
        /// List of parameters for use in Policy and Policy/Set (previously GetConfigValuesCommand/SetConfigValuesCommand).
        /// A Dictionary of key,value pairs indicating the valid parameter names and data types that can be set for the device.
        /// </summary>
        public Dictionary<string, string> ConfigParameters { get; set; }

        /// <summary>
        /// List of sensor reading measures and their associated units.
        /// A Dictionary of key,value pairs indicating the valid sensor reading names and measurement units that can be set for the device.
        /// </summary>
        public Dictionary<string, string> SensorReadingMeasures { get; set; }

    }
}
