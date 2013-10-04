using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TersoSolutions.Jetstream.SDK.Application.Messages
{
    public abstract class JetstreamEvent
    {
        public string EventType { get; set; }
        public string EventId { get; set; }
        public DateTime EventTime { get; set; }
    }
}
