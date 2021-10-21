using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Confluent.Kafka;

namespace CT.SC.Foundation.Kafka.Models
{
    public class ProducerModel: IKafkaSettings 
    {
        public ProducerModel()
        {
        }
    }
    public enum SecurityProtocolValue
    {
        //
        // Summary:
        //     Plaintext
        Plaintext = 0,
        //
        // Summary:
        //     Ssl
        Ssl = 1,
        //
        // Summary:
        //     SaslPlaintext
        SaslPlaintext = 2,
        //
        // Summary:
        //     SaslSsl
        SaslSsl = 3
    }

    //
    // Summary:
    //     SaslMechanism enum values
    public enum SaslMechanismValue
    {
        //
        // Summary:
        //     GSSAPI
        Gssapi = 0,
        //
        // Summary:
        //     PLAIN
        Plain = 1,
        //
        // Summary:
        //     SCRAM-SHA-256
        ScramSha256 = 2,
        //
        // Summary:
        //     SCRAM-SHA-512
        ScramSha512 = 3,
        //
        // Summary:
        //     OAUTHBEARER
        OAuthBearer = 4
    }

    //
    // Summary:
    //     AutoOffsetReset enum values
    public enum AutoOffsetResetValue
    {
        //
        // Summary:
        //     Latest
        Latest = 0,
        //
        // Summary:
        //     Earliest
        Earliest = 1,
        //
        // Summary:
        //     Error
        Error = 2
    }
}