[![GitHub license](https://img.shields.io/github/license/amitkumar-ak/sitecore-kafka-integration.svg)](https://github.com/amitkumar-ak/sitecore-kafka-integration/blob/master/LICENSE)
[![GitHub contributors](https://img.shields.io/github/contributors/amitkumar-ak/sitecore-kafka-integration.svg)](https://GitHub.com/amitkumar-ak/sitecore-kafka-integration/graphs/contributors/)
[![GitHub issues](https://img.shields.io/github/issues/amitkumar-ak/sitecore-kafka-integration.svg)](https://GitHub.com/amitkumar-ak/sitecore-kafka-integration/issues/)
[![GitHub pull-requests](https://img.shields.io/github/issues-pr/amitkumar-ak/sitecore-kafka-integration.svg)](https://GitHub.com/amitkumar-ak/sitecore-kafka-integration/pulls/)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)

[![GitHub watchers](https://img.shields.io/github/watchers/amitkumar-ak/sitecore-kafka-integration.svg?style=social&label=Watch&maxAge=2592000)](https://GitHub.com/amitkumar-ak/sitecore-kafka-integration/watchers/)
[![GitHub forks](https://img.shields.io/github/forks/amitkumar-ak/sitecore-kafka-integration.svg?style=social&label=Fork&maxAge=2592000)](https://GitHub.com/amitkumar-ak/sitecore-kafka-integration/network/)
[![GitHub stars](https://img.shields.io/github/stars/amitkumar-ak/sitecore-kafka-integration.svg?style=social&label=Star&maxAge=2592000)](https://GitHub.com/amitkumar-ak/sitecore-kafka-integration/stargazers/)

# Sitecore Kafka Integration
Sitecore and Kafka Integration using ASP.NET MVC Application.

We can integrate Sitecore with Kafka using .NET Connectors. I will be using Cloudera .NET client library for Apache Kafka called [**Cloudera.Kafka**](https://docs.cloudera.com/runtime/7.2.8/kafka-developing-applications/topics/kafka-develop-dotnet.html).

The **Cloudera.Kafka** assembly can be downloaded and installed through Visual Studio or a command line interface or from nuget.org.
Using Cloudera.Kafka, we can Push the data to Kafka system using ProducerBuilder and Pull using ConsumerBuilder.

# Getting Started

> **Download**, the solution on your local system and restore the NuGet packages.

> **Kafka Configurations**, to use the `Kafka` we have to change the `Kafka configurations` in the following places:
> - **\Sitecore-Kafka-Integration\KafkaIntegrationSite\KafkaIntegrationSite\Controllers\KafkaController.cs >** `PublishToKafka` **Function :-** 
```csharp
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
```

**[⬆ Back To Top](#sitecore-kafka-integration)**

> - **\Sitecore-Kafka-Integration\KafkaIntegrationSite\KafkaIntegrationSite\Controllers\KafkaController.cs >** `PullFromKafka` **Function :-** 
```csharp
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
```
**[⬆ Back To Top](#sitecore-kafka-integration)**

## Output

### Landing Page
- Once you `RUN` the application, you will land into the `HOME` page:
   <img src="images/Sitecore-Kafka-Integration-HomePage.PNG" style="max-width:100%;">
   
   **[⬆ Back To Top](#sitecore-kafka-integration)**
   
### Push message from Sitecore to Kafka   
- You can `PUSH` the message to the `Kafka` topic:
   <img src="images/Sitecore-Kafka-Integration-Produce-Message-1.PNG" width="800" height="700" style="max-width:100%;">
   
**[⬆ Back To Top](#sitecore-kafka-integration)**

   > Once user click on the `Push to Kafka`, user will see `Please Wait` screen:
   <img src="images/Sitecore-Kafka-Integration-PleaseWait.PNG" style="max-width:100%;">

**[⬆ Back To Top](#sitecore-kafka-integration)**

   > After successful `Push` user will see `Scuccess` message:
   <img src="images/Sitecore-Kafka-Integration-Produce-Message-Added.PNG" style="max-width:100%;">

**[⬆ Back To Top](#sitecore-kafka-integration)**
   
### Pull message from Kafka to Sitecore  
- You can `PULL` the message from `Kafka` topic:
   <img src="images/Sitecore-Kafka-Integration-Consume-Message-1.PNG" style="max-width:100%;">
   
**[⬆ Back To Top](#sitecore-kafka-integration)**

   > Once user click on the `Pull from Kafka`, user will see `Please Wait` screen:
   <img src="images/Sitecore-Kafka-Integration-PleaseWait.PNG" style="max-width:100%;">

**[⬆ Back To Top](#sitecore-kafka-integration)**

   > If `Messages` not present in `Kafka Topic` user will see `Error` message:
   <img src="images/Sitecore-Kafka-Integration-Consume-Message-Not-Present.PNG" style="max-width:100%;">

**[⬆ Back To Top](#sitecore-kafka-integration)**

   > If `Messages` present in `Kafka Topic` user will see `Success` message with details:
   <img src="images/Sitecore-Kafka-Integration-Consume-Message-Present.PNG" style="max-width:100%;">

**[⬆ Back To Top](#sitecore-kafka-integration)**

