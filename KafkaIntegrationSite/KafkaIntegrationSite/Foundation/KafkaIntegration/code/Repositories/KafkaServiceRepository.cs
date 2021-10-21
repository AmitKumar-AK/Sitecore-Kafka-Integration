using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CT.SC.Foundation.Kafka.Models;
using CT.SC.Foundation.Kafka.Services;

namespace CT.SC.Foundation.Kafka.Repositories
{
    public class KafkaServiceRepository : IKafkaServiceRepository
    {
        public virtual KafkaService Kafka(IKafkaSettings kafkaSettings)
        {
            return new KafkaService(kafkaSettings);
        }
    }
}