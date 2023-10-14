using Confluent.Kafka;
using CQRS.Core.Events;
using Kafka.Core.Abstract;
using Kafka.Core.Options;
using KafkaFlow.Producers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Kafka.Core.Services.Producer
{
    public class EventProducer : IEventProducer
    {
        private readonly KafkaProducerOptions _config;
        private readonly IProducerAccessor _producerAccessor;

        public EventProducer(IOptions<KafkaProducerOptions> config, IProducerAccessor producerAccessor)
        {
            _config = config.Value;
            _producerAccessor = producerAccessor;
        }

        public async Task ProduceAsync<T>(T @event) where T : Event
        {
            var producer = _producerAccessor.GetProducer(_config.ProducerName);
            await producer.ProduceAsync(_config.ProducerTopic, Guid.NewGuid().ToString(), @event);
        }

        public async Task ProduceAsync<T>(params T[] @events) where T : Event
        {
            var producer = _producerAccessor.GetProducer(_config.ProducerName);

            foreach (var @event in @events)
            {
                await producer.ProduceAsync(_config.ProducerTopic, Guid.NewGuid().ToString(), @event);
            }
        }
    }
}
