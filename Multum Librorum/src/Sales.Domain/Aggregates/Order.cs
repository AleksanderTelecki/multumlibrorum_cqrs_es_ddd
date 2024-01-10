using Marte.EventSourcing.Core.Aggregate;
using Sales.Messages.Enums;
using Sales.Messages.Events;
using Sales.Messages.Models;

namespace Sales.Domain.Aggregates;

public class Order: AggregateRoot
{
    public Guid ClientId { get; protected set; }
    public List<OrderItem> Items { get; protected set; }
    public OrderState State { get; protected set; }

    public Order(Guid clientId, Guid orderId)
    {
        Id = orderId;
        
        RaiseEvent(new OrderCreatedEvent
        {
            Id = Id,
            ClientId = clientId
        });
    }

    public Order()
    {
        
    }
    
    public void AddProductsToOrder(ICollection<OrderItem> orderItems)
    {
        RaiseEvent(new OrderProductsAddedEvent()
        {
            Id = Id,
            OrderItems = orderItems
        });
    }

    public void MarkAsPlaced()
    {
        RaiseEvent(new OrderPlacedEvent()
        {
            Id = Id,
        }); 
    }
    
    public void Apply(OrderPlacedEvent @event)
    {
        State = OrderState.Placed;

        Version++;
    }
    
    public void Apply(OrderProductsAddedEvent @event)
    {
        Items = @event.OrderItems.ToList();

        Version++;
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
        State = @event.NewOrderState;

        Version++;
    }
    
}