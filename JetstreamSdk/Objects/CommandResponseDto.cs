using System.Collections.Generic;

namespace TersoSolutions.Jetstream.SDK.Objects
{
    /// <summary>
    /// Provides information about a command
    /// that is queued to a device
    /// </summary>
    public class CommandResponseDto
    {
        /// <summary>
        /// The ID for the command that is
        /// send to the device
        /// </summary>
        public string CommandId { get; set; }

        /// <summary>
        /// Indicates whether a command is 
        /// queued or processed when submitted
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// A list of exceptions that occurred during 
        /// the execution of a command
        /// </summary>
        public IList<KeyValuePair<string, string>> ExceptionList { get; set; }

        /// <summary>
        /// Parameter List passed back from the execution
        /// of a Jetstream command
        /// </summary>
        public IList<KeyValuePair<string, string>> OutputParameterList { get; set; }
    }
}
