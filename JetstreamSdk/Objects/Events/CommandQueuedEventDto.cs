using System.Collections.Generic;

namespace TersoSolutions.Jetstream.SDK.Objects.Events
{
    /// <summary>
    /// An event created in Jetstream to indicate that a 
    /// device command has been created and queued
    /// </summary>
    public class CommandQueuedEventDto : EventDto
    {
        /// <summary>
        /// The LogicalDeviceId for which the 
        /// event command is queued
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// A unique identifier for the event
        /// </summary>
        public string CommandId { get; set; }

        /// <summary>
        /// Name of the command that was queued
        /// for sending to the device
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// Uniform resource identifier that
        /// was called in order to created this command
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// The HTTP request method's verb used
        /// to create this command queued event
        /// </summary>
        public string Verb { get; set; }

        /// <summary>
        /// Name of the user that queued
        /// the device command
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// An IList of parameters sent to
        /// the device in the command
        /// </summary>
        public IList<KeyValuePair<string, string>> Parameters { get; set; }
    }
}
