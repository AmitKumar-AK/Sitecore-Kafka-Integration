using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CT.SC.Foundation.Kafka.Models;
using CT.SC.Foundation.Kafka.Services;

namespace CT.SC.Foundation.Kafka.Repositories
{
    public interface IKafkaServiceRepository
    {
        KafkaService Kafka(IKafkaSettings kafkaSettings);
    }
}