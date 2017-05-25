using System.Collections.Generic;

namespace TersoSolutions.Jetstream.SDK.Objects.Events
{
    /// <summary>
    /// Event sent by device when command is finished.
    /// Contains result parameters.
    /// </summary>
    public class CommandCompletionEventDto : DeviceEventDto
    {
        /// <summary>
        /// Unique identifier for a command queued event
        /// </summary>
        public string CommandId { get; set; }

        /// <summary>
        /// The name of the command sent to the device
        /// </summary>
        public string CommandName { get; set; }
        
        /// <summary>
        /// Output returned from the device
        /// </summary>
        public IList<KeyValuePair<string, string>> OutputParameterList { get; set; }

        /// <summary>
        /// Exceptions returned from the device
        /// </summary>
        public IList<KeyValuePair<string, string>> ExceptionList { get; set; }
    }
}
