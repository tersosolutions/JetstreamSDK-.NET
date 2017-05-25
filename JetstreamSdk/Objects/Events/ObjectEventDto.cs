using System.Collections.Generic;

namespace TersoSolutions.Jetstream.SDK.Objects.Events
{
    /// <summary>
    /// DTO for Object Events from the device
    /// </summary>
    public class ObjectEventDto : DeviceEventDto
    {
        /// <summary>
        /// EPCs observed by the device
        /// </summary>
        public IList<string> Observe { get; set; }
    }
}
