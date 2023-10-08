using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Events;
using CQRS.Core.Infrastructure;
using CQRS.Core.JsonConverters;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CQRS.Core.Kafka
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly IEventDispatcher _eventDispatcher;

        public EventConsumer(IOptions<ConsumerConfig> config, IEventDispatcher eventDispatcher)
        {
            _config = config.Value;
            _eventDispatcher = eventDispatcher;
        }

        public void Consume(string topic)
        {
            using var consumer = new ConsumerBuilder<string, string>(_config)
                        .SetKeyDeserializer(Deserializers.Utf8)
                        .SetValueDeserializer(Deserializers.Utf8)
                        .Build();

            consumer.Subscribe(topic);

            while (true)
            {
                var consumeResult = consumer.Consume();

                if (consumeResult?.Message == null) continue;

                var options = new JsonSerializerOptions { Converters = { new EventJsonConverter() } };
                var @event = JsonSerializer.Deserialize<Event>(consumeResult.Message.Value, options);

                _eventDispatcher.On(@event);
                consumer.Commit(consumeResult);
            }
        }
    }
}
