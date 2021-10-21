using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CT.SC.Foundation.Kafka.Models
{
    public class IKafkaSettings
    {
        public string Topic { get; set; }
        public string Message { get; set; }


        /// <summary>
        ///    Initial list of brokers as a CSV list of broker host or host:port. The application
        ///     may also use `rd_kafka_brokers_add()` to add brokers during runtime. default:
        ///     '' importance: high
        /// </summary>
        public string BootstrapServers { get; set; }

        /// <summary>
        ///     Protocol used to communicate with brokers. default: plaintext importance: high
        /// </summary>
        public SecurityProtocolValue? SecurityProtocol { get; set; }

        /// <summary>
        ///    SASL mechanism to use for authentication. Supported: GSSAPI, PLAIN, SCRAM-SHA-256,
        ///     SCRAM-SHA-512. **NOTE**: Despite the name, you may not configure more than one
        ///    mechanism.
        /// </summary>
        public SaslMechanismValue? SaslMechanism { get; set; }

        /// <summary>
        ///     File or directory path to CA certificate(s) for verifying the broker's key. Defaults:
        ///     On Windows the system's CA certificates are automatically looked up in the Windows
        ///     Root certificate store. On Mac OSX this configuration defaults to `probe`. It
        ///    is recommended to install openssl using Homebrew, to provide CA certificates.
        ///    On Linux install the distribution's ca-certificates package. If OpenSSL is statically
        ///    linked or `ssl.ca.location` is set to `probe` a list of standard paths will be
        ///    probed and the first one found will be used as the default CA certificate location
        ///    path. If OpenSSL is dynamically linked the OpenSSL library's default path will
        ///    be used (see `OPENSSLDIR` in `openssl version -a`). default: '' importance: low
        /// </summary>
        public string SslCaLocation { get; set; }

        /// <summary>
        ///     SASL username for use with the PLAIN and SASL-SCRAM-.. mechanisms default: ''
        ///     importance: high
        /// </summary>
        public string SaslUsername { get; set; }

        /// <summary>
        ///    SASL password for use with the PLAIN and SASL-SCRAM-.. mechanism default: ''
        ///     importance: high
        /// </summary>
        public string SaslPassword { get; set; }

        /// <summary>
        ///     Enable OpenSSL's builtin broker (server) certificate verification. This verification
        ///    can be extended by the application by implementing a certificate_verify_cb. default:
        ///    true importance: low        /// </summary>
        public bool? EnableSslCertificateVerification { get; set; }

        public Confluent.Kafka.ProducerConfig PushSettings { get; set; }
        public Confluent.Kafka.ConsumerConfig PullSettings { get; set; }
    }
}