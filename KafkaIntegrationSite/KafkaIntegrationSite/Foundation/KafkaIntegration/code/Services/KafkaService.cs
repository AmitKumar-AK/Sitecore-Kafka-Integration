using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Confluent.Kafka;
using CT.SC.Foundation.Kafka.Models;

namespace CT.SC.Foundation.Kafka.Services
{
    public class KafkaService
    {
        public virtual IKafkaSettings Settings { get; set; }
        private KafkaResult Response;
        public KafkaService(IKafkaSettings settings)
        {
            this.Settings = settings;
            this.Response = new KafkaResult();
            this.Response.Messages = new List<string>();
        }

        public virtual KafkaResult Push()
        {
            ProducerConfig producerConfig =  new ProducerConfig
           {
                #region Kafka Authentication and Authorization settings
                /*Generally you will have more than one BootstrapServers and you can mention all of them like 
                "env-kafka-broker0.cloudera.site:1014, env-kafka-broker1.cloudera.site:1014, env-kafka-broker2.cloudera.site:1014"
                if you want to access single broker only then "env-kafka-broker0.cloudera.site:1014" 
                */
               BootstrapServers = this.Settings.BootstrapServers,
               SecurityProtocol = (SecurityProtocol)this.Settings.SecurityProtocol,
               SaslMechanism = (SaslMechanism)this.Settings.SaslMechanism,
               // SSL Certificate Location, you can provide the path from your server
               SslCaLocation = this.Settings.SslCaLocation,
               // SSL User Name
               SaslUsername = this.Settings.SaslUsername,
               // SSL Password
               SaslPassword = this.Settings.SaslPassword,
               //Debug = "all", - To show the more logs
               EnableSslCertificateVerification = this.Settings.EnableSslCertificateVerification
                #endregion
            };
            if (!string.IsNullOrEmpty(this.Settings.Topic) && !string.IsNullOrEmpty(this.Settings.Message))
            {
                /*
                After creating the Cloudera .NET client ProducerConfig object in Sitecore code base 
                then we can push data from Sitecore to Kafka using Cloudera .NET client ProducerBuilder.
                this.Settings.Topic = The Kafka Topic to which message needs to be send from Sitecore
                this.Settings.Message = The message which to need to send to Kafka for storage from Sitecore
                 */
                using (var producer =
                     new Confluent.Kafka.ProducerBuilder<Null, string>(producerConfig).Build())
                {
                    try
                    {
                        producer.Produce(this.Settings.Topic, new Message<Null, string> { Value = this.Settings.Message }, this.DeliveryReportHandler);
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Oops, something went wrong while publishing message to Kafka: {e}");
                    }
                    producer.Flush();
                }
            }

            return this.Response;
        }

        public virtual KafkaResult Pull()
        {
            dynamic d = this.Settings;
            /*
            To pull data from Kafka to Sitecore, we first need to create the ConsumerConfig object 
            using Cloudera .NET client 
            */
            ConsumerConfig consumerConfig = new ConsumerConfig
            {
                #region Kafka Authentication and Authorization settings
                /* The GroupId property is mandatory and specifies which 
                    consumer group the consumer is a member of
                */
                GroupId = d.GroupId,
                /*
                    The AutoOffsetReset property specifies what offset the consumer should 
                    start reading from in the event there are no committed offsets for a partition, 
                    or the committed offset is invalid (perhaps due to log truncation).                 
                */
                AutoOffsetReset = (AutoOffsetReset)d.AutoOffsetReset,
                EnablePartitionEof = d.EnablePartitionEof,
                /* Generally you will have more than one BootstrapServers and you can mention all of them like 
                    "env-kafka-broker0.cloudera.site:1014, env-kafka-broker1.cloudera.site:1014, env-kafka-broker2.cloudera.site:1014"
                    if you want to access single broker only then "env-kafka-broker0.cloudera.site:1014" 
                */
                BootstrapServers = this.Settings.BootstrapServers,
                SecurityProtocol = (SecurityProtocol)this.Settings.SecurityProtocol,
                SaslMechanism = (SaslMechanism)this.Settings.SaslMechanism,
                // SSL Certificate Location, you can provide the path from your server
                SslCaLocation = this.Settings.SslCaLocation,
                // SSL User Name
                SaslUsername = this.Settings.SaslUsername,
                // SSL Password
                SaslPassword = this.Settings.SaslPassword,
                //Debug = "all", - To show the more logs
                EnableSslCertificateVerification = this.Settings.EnableSslCertificateVerification
                #endregion
            };
            if (!string.IsNullOrEmpty(this.Settings.Topic) && !string.IsNullOrEmpty(d.GroupId))
            {
                /*
                After creating the Cloudera .NET client ConsumerConfig object in Sitecore code base 
                then we can pull data from Kafka to Sitecore using Cloudera .NET client ConsumerBuilder.
                this.Settings.Topic = The Kafka Topic to which message needs to be send from Sitecore
                this.Settings.Message = The message which to need to send to Kafka for storage from Sitecore
                 */

                using (var c = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
                {
                    /*
                        A typical Kafka consumer application is centered around a consume loop, 
                        which repeatedly calls the Consume method to retrieve records one-by-one 
                        that have been efficiently pre-fetched by the consumer in background threads. 
                        Before entering the consume loop, you’ll typically use 
                        the Subscribe method to specify which topics should be fetched from.
                        Source: https://docs.confluent.io/clients-confluent-kafka-dotnet/current/overview.html#the-consume-loop
                     */
                    c.Subscribe(this.Settings.Topic);
                    try
                    {
                        string key = string.Empty;
                        while (true)
                        {
                            var cr = c.Consume(120000);
                            if (cr !=null && cr.Message != null)
                            {
                                key = cr.Message.Key == null ? "Null" : Convert.ToString(cr.Message.Key);
                                this.UpdateResponse(true, "Consumed record with key: " + key + " Message: " + cr.Message.Value + " received from Partition Offset: " + cr.TopicPartitionOffset);
                            }
                            else
                            {
                                if(!this.Response.IsSuccess)
                                {
                                    //--if no message added
                                    this.UpdateResponse(false, "Message not present for the topic " + this.Settings.Topic);
                                }
                                break;
                            }
                        }
                    }
                    catch (ConsumeException e)
                    {
                        this.UpdateResponse(false, "Consume error: " + e.Error.Reason);
                    }
                    catch (OperationCanceledException )
                    {
                        //exception might have occurred.
                    }
                    catch (Exception e)
                    {
                        this.UpdateResponse(false, "Unexpected error: " + e.StackTrace);
                    }
                    finally
                    {
                        // Ensure the consumer leaves the group cleanly and final offsets are committed.
                        c.Close();
                    }
                }
            }

            return this.Response;
        }

        public void DeliveryReportHandler(DeliveryReport<Null, string> deliveryReport)
        {
            if (deliveryReport.Status == PersistenceStatus.NotPersisted)
            {
                this.Response.IsSuccess = false;
                this.Response.Messages.Add("Error::Message delivery Failed=>" + deliveryReport.Message.Value);
            }
            else if (deliveryReport.Status == PersistenceStatus.Persisted)
            {
                this.Response.IsSuccess = true;
                this.Response.Messages.Add("Success::Message delivery Success=>" + deliveryReport.Message.Value);
            }
        }

        public void UpdateResponse(bool isSuccess, string details)
        {
            this.Response.IsSuccess = isSuccess;
            this.Response.Messages.Add(details);

        }
    }

}