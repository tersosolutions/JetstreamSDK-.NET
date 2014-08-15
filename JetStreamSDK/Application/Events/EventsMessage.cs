using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TersoSolutions.Jetstream.SDK.Application.Events
{
    /// <summary>
    /// This class defines the properties of a JetStream
    /// events message.
    /// </summary>
    /// <remarks>Author Mark Neustadt</remarks>
    public class EventsMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Notification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String MessageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String TopicArn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Subject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Timestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String SignatureVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Signature { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UnsubscribeURL { get; set; }
    }
}
