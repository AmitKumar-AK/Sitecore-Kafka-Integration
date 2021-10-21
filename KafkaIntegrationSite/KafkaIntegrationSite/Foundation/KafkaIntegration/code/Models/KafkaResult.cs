using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CT.SC.Foundation.Kafka.Models
{
    public class KafkaResult
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsSuccess { get; set; }
        public string Topic { get; set; }
        public List<string> Messages { get; set; }
    }
}