using CQRS.Core.Events;

namespace Sales.Messages.Events;

public class OrderCreatedEvent: Event
{
    public Guid ClientId { get; set; }
}