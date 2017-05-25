
namespace TersoSolutions.Jetstream.SDK.Objects.Events
{
    /// <summary>
    /// The Logical Device Added Event data sent when a device is added
    /// to an application using the POST HTTP verb on v2 Devices
    /// </summary>
    public class LogicalDeviceAddedEventDto : EventDto
    {
        /// <summary>
        /// The friendly name of the device
        /// </summary>
        public string LogicalDeviceId { get; set; }

        /// <summary>
        /// The name of the Device Definition the device is associated with
        /// </summary>
        public string DeviceDefinition { get; set; }

        /// <summary>
        /// The serial number of the device
        /// </summary>
        public string DeviceSerialNumber { get; set; }

        /// <summary>
        /// The region of the device
        /// </summary>
        public string Region { get; set; }
    }
}
