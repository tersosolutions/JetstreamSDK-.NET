using System.Collections.Generic;

namespace TersoSolutions.Jetstream.SDK.Objects.Events
{
    /// <summary>
    /// DTO for log entry events sent by device
    /// </summary>
    public class LogEntryEventDto : DeviceEventDto
    {
        /// <summary>
        /// List of log entries inside the event
        /// </summary>
        public IList<LogEntryDto> LogEntries { get; set; }
        
    }

    /// <summary>
    /// Custom class for use inside the log entry event class
    /// </summary>
    public class LogEntryDto
    {
        /// <summary>
        /// Level of event that occured (Warning or Error as examples)
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// Message recorded to device's log
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Type of event
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Time of logging
        /// </summary>
        public string LogTime { get; set; }
    }
}
