using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TersoSolutions.Jetstream.SDK.Application.SQS
{
    public class AmazonSNSMessage
    {
        public String Type { get; set; }
        public String Notification { get; set; }
        public String MessageId { get; set; }
        public String TopicArn { get; set; }
        public String Subject { get; set; }
        public String Message { get; set; }
        public String Timestamp { get; set; }
        public String SignatureVersion { get; set; }
        public String Signature { get; set; }
        public String UnsubscribeURL { get; set; }
    }
}
