using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Core.Options
{
    public class KafkaConsumerOptions
    {
        public IEnumerable<string> Topics { get; set; }
        public string TopicGroup { get; set; }
    }
}
