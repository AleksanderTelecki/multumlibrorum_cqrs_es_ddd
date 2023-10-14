using KafkaFlow.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Core.Extensions
{
    public static class KafkaFlowExtensions
    {
        public static IClusterConfigurationBuilder AddTopicAutoGeneration(this IClusterConfigurationBuilder builder, IEnumerable<string> topics)
        {
            foreach (var topic in topics)
            {
                builder.CreateTopicIfNotExists(topic, 1, 1);
            }

            return builder;
        }
    }
}
