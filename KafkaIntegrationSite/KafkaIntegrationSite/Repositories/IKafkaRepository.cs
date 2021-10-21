using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CT.SC.Foundation.Kafka.Models;

namespace KafkaIntegrationSite.Repositories
{
    public interface IKafkaRepository
    {
        KafkaResult Push(ProducerModel settings);

        KafkaResult Pull(ConsumerModel settings);
    }
}