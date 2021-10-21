using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CT.SC.Foundation.Kafka.Models;
using CT.SC.Foundation.Kafka.Repositories;

namespace KafkaIntegrationSite.Repositories
{
    public class KafkaRepository: IKafkaRepository
    {
        private readonly IKafkaServiceRepository kafkaServiceRepository;

        public KafkaRepository()
        {
            this.kafkaServiceRepository = new KafkaServiceRepository();
        }
        public KafkaRepository(IKafkaServiceRepository kafkaServiceRepository)
        {
            this.kafkaServiceRepository = kafkaServiceRepository;
        }

        public KafkaResult Push(ProducerModel settings)
        {
            KafkaResult result;

            var kafkaService = this.kafkaServiceRepository.Kafka(settings);
            result = kafkaService.Push();
            return result;

        }

        public KafkaResult Pull(ConsumerModel settings)
        {
            KafkaResult result;

            var kafkaService = this.kafkaServiceRepository.Kafka(settings);
            result = kafkaService.Pull();
            return result;

        }
    }
}