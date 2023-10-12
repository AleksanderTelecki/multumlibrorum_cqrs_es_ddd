using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Kafka.Options
{
    public class KafkaConsumerOptions
    {
        public IEnumerable<string> Topics { get; set; }
    }
}
