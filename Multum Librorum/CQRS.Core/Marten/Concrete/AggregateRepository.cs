using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Marten.Abstract;
using CQRS.Core.Producers;
using Marten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Marten.Concrete
{
    public sealed class AggregateRepository: IAggregateReporitory
    {
        private readonly IDocumentStore store;
        private readonly IEventProducer eventProducer;

        public AggregateRepository(IDocumentStore store, IEventProducer eventProducer)
        {
            this.store = store;
            this.eventProducer = eventProducer;
        }

        public async Task StoreAsync(AggregateRoot aggregate, CancellationToken ct = default)
        {
            await using var session = await store.LightweightSerializableSessionAsync(token: ct);
            // Take non-persisted events, push them to the event stream, indexed by the aggregate ID
            var events = aggregate.GetUncommittedEvents().ToArray();
            session.Events.Append(aggregate.Id, aggregate.Version, events);
            await session.SaveChangesAsync(ct);

            var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
            await eventProducer.ProduceAsync(topic, events);

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
