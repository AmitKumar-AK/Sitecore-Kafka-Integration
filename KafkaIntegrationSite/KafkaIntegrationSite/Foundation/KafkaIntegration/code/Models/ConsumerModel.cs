using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CT.SC.Foundation.Kafka.Models
{
    public class ConsumerModel : IKafkaSettings
    {
        public ConsumerModel()
        {
        }

        /// <summary>
        ///     Client group id string. All clients sharing the same group.id belong to the same
        ///     group. default: '' importance: high        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        ///     Action to take when there is no initial offset in offset store or the desired
        ///     offset is out of range: 'smallest','earliest' - automatically reset the offset
        ///     to the smallest offset, 'largest','latest' - automatically reset the offset to
        ///    the largest offset, 'error' - trigger an error (ERR__AUTO_OFFSET_RESET) which
        ///     is retrieved by consuming messages and checking 'message->err'. default: largest
        ///     importance: high
        /// </summary>
        public AutoOffsetResetValue? AutoOffsetReset { get; set; }

        /// <summary>
        ///     Emit RD_KAFKA_RESP_ERR__PARTITION_EOF event whenever the consumer reaches the
        ///    end of a partition. default: false importance: low
        /// </summary>
        public bool? EnablePartitionEof { get; set; }
    }
}