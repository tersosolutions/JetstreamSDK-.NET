using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TersoSolutions.Jetstream.SDK.Application.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class JetstreamEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EventTime { get; set; }
    }
}
