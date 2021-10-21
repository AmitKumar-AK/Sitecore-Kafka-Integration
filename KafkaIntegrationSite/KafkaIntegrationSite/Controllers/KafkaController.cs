using System.Web.Mvc;
using CT.SC.Foundation.Kafka.Models;
using KafkaIntegrationSite.Repositories;
using Newtonsoft.Json.Linq;


namespace KafkaIntegrationSite.Controllers
{
    public class KafkaController : Controller
    {
        private IKafkaRepository kafkaRepository { get; }

        public KafkaController()
        {
            this.kafkaRepository = new KafkaRepository();
        }
        public KafkaController(IKafkaRepository kafkaRepository)
        {
            this.kafkaRepository = kafkaRepository;
        }

        [HttpPost]
        public JsonResult PublishToKafka(string kafkaData)
        {
            var objKafka = JToken.Parse(kafkaData);
            string topic = (string)objKafka["topic"];
            string message = (string)objKafka["message"];

            var items = this.kafkaRepository.Push(
                new CT.SC.Foundation.Kafka.Models.ProducerModel
                {
                    Topic = topic,
                    Message = message,
                    BootstrapServers = "<Fill details from configuration items>",
                    SecurityProtocol = SecurityProtocolValue.SaslSsl,
                    SaslMechanism = SaslMechanismValue.Plain,
                    SslCaLocation = @"<Fill details from configuration items>",
                    SaslUsername = "<Fill details from configuration items>",
                    SaslPassword = "<Fill details from configuration items>",
                    EnableSslCertificateVerification = false
                }
                );
           
            if(items.IsSuccess)
            {
                return Json(new { result = items.IsSuccess, type = "add", value = items.Messages });
            }
            else
            {
                return Json(new { result = items.IsSuccess, type = "add", value = items.Messages });

            }
        }

        [HttpPost]
        public JsonResult PullFromKafka(string kafkaData)
        {
            var objKafka = JToken.Parse(kafkaData);
            string topic = (string)objKafka["topic"];
            string groupId = (string)objKafka["groupId"];

            var items = this.kafkaRepository.Pull(
                new CT.SC.Foundation.Kafka.Models.ConsumerModel
                {
                    Topic = topic,
                    GroupId = groupId,
                    AutoOffsetReset = AutoOffsetResetValue.Earliest,
                    EnablePartitionEof = true,
                    BootstrapServers = "<Fill details from configuration items>",
                    SecurityProtocol = SecurityProtocolValue.SaslSsl,
                    SaslMechanism = SaslMechanismValue.Plain,
                    SslCaLocation = @"<Fill details from configuration items>",
                    SaslUsername = "<Fill details from configuration items>",
                    SaslPassword = "<Fill details from configuration items>",
                    EnableSslCertificateVerification = false
                }
                );

            if (items.IsSuccess)
            {
                return Json(new { result = items.IsSuccess, type = "get", value = items.Messages });
            }
            else
            {
                return Json(new { result = items.IsSuccess, type = "get", value = items.Messages });

            }
        }
    }
}