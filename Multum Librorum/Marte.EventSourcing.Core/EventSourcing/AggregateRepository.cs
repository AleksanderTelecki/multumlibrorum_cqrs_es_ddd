using Kafka.Core.Abstract;
using Kafka.Core.Options;
using Marte.EventSourcing.Core.Abstract;
using Marte.EventSourcing.Core.Aggregate;
using Microsoft.Extensions.Options;

namespace Marten.EventSourcing.Core
{
    public sealed class AggregateRepository: IAggregateReporitory
    {
        private readonly IDocumentStore store;
        private readonly IEventProducer eventProducer;
        private readonly IOptions<KafkaProducerOptions> kafkaProducerOptions;

        public AggregateRepository(IDocumentStore store, IEventProducer eventProducer, IOptions<KafkaProducerOptions> kafkaProducerOptions)
        {
            this.store = store;
            this.eventProducer = eventProducer;
            this.kafkaProducerOptions = kafkaProducerOptions;
        }

        public async Task StoreAsync(AggregateRoot aggregate, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(kafkaProducerOptions.Value.ProducerTopic))
                throw new ArgumentNullException("Producer kafka topic is null or empty.");

            await using var session = await store.LightweightSerializableSessionAsync(token: ct);
            // Take non-persisted events, push them to the event stream, indexed by the aggregate ID
            var events = aggregate.GetUncommittedEvents().ToArray();
            session.Events.Append(aggregate.Id, aggregate.Version, events);
            await session.SaveChangesAsync(ct);
            
            // Produce event with kafka
            await eventProducer.ProduceAsync(events);

            // Once successfully persisted, clear events from list of uncommitted events
            aggregate.ClearUncommittedEvents();

        }

        public async Task<T> LoadAsyncVersion<T>(Guid id, int? version = null, CancellationToken ct = default) where T : AggregateRoot
        {
            await using var session = await store.LightweightSerializableSessionAsync(token: ct);
            var aggregate = await session.Events.AggregateStreamAsync<T>(id, version ?? 0, token: ct);
            return aggregate ?? throw new InvalidOperationException($"No aggregate by id {id}.");
        }

        public async Task<T> LoadAsync<T>(Guid id, CancellationToken ct = default) where T : AggregateRoot
        {
            await using var session = await store.LightweightSerializableSessionAsync(token: ct);
            var aggregate = await session.Events.AggregateStreamAsync<T>(id, token: ct);
            return aggregate ?? throw new InvalidOperationException($"No aggregate by id {id}.");
        }
    }
}
