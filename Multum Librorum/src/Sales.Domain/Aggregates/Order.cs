using Marte.EventSourcing.Core.Aggregate;
using Sales.Messages.Enums;
using Sales.Messages.Events;
using Sales.Messages.Models;

namespace Sales.Domain.Aggregates;

public class Order: AggregateRoot
{
    public Guid ClientId { get; set; }
    public List<OrderItem> Items { get; set; }
    
    public OrderState State { get; set; }

    public Order(Guid clientId)
    {
        Id = Guid.NewGuid();

        RaiseEvent(new OrderCreatedEvent
        {
            Id = Id,
            ClientId = clientId
        });
    }

    public Order()
    {
        
    }
    
    public void Apply(OrderCreatedEvent @event)
    {
        Id = @event.Id;
        ClientId = @event.ClientId;
        Items = new List<OrderItem>();

        Version++;
    }

    public void UpdateOrderState(OrderState orderState)
    {
        RaiseEvent(new OrderStateUpdatedEvent
        {
            Id = Id,
            NewOrderState = orderState
        });
    }
    
    public void Apply(OrderStateUpdatedEvent @event)
    {
        Id = @event.Id;
        State = @event.NewOrderState;

        Version++;
    }
    
}