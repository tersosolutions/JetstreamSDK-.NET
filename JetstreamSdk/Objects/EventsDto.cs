using System.Collections.Generic;
using TersoSolutions.Jetstream.SDK.Objects.Events;

namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// Contains data to be utilized about a set
    /// of Jetstream events 
    /// </summary>
    public class EventsDto
    {
        /// <summary>
        /// The batch id for the list of events
        /// </summary>
        public string BatchId { get; set; }

        /// <summary>
        /// The count of events in the DTO
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The list of events
        /// </summary>
        public List<EventDto> Events { get; set; }

    }
}
