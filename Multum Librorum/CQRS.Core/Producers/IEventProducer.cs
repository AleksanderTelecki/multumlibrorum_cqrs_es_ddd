using CQRS.Core.Events;

namespace CQRS.Core.Producers
{
    public interface IEventProducer
    {
        Task ProduceAsync<T>(string topic, T @event) where T : Event;

        Task ProduceAsync<T>(string topic, params T[] @event) where T : Event;
    }
}
