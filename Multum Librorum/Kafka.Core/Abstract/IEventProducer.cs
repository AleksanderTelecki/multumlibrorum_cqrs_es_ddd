using CQRS.Core.Events;

namespace Kafka.Core.Abstract
{
    public interface IEventProducer
    {
        Task ProduceAsync<T>(T @event) where T : Event;

        Task ProduceAsync<T>(params T[] @event) where T : Event;
    }
}
