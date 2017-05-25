using System.Collections.Generic;

namespace TersoSolutions.Jetstream.SDK.Objects.Events
{
    /// <summary>
    /// DTO for Aggregate Events from the device
    /// </summary>
    public class AggregateEventDto : DeviceEventDto
    {
        /// <summary>
        /// Pass associated with transaction
        /// </summary>
        public string PassRfid { get; set; }

        /// <summary>
        /// EPCs added in this transaction
        /// </summary>
        public IList<string> Adds { get; set; }

        /// <summary>
        /// EPCs removed in this transaction
        /// </summary>
        public IList<string> Removes { get; set; }
    }
}
