using CQRS.Core.Events.Abstract;
using Sales.Domain.Repository;
using Sales.Messages.Events;

namespace Sales.Domain.EventHandlers;

public class OrderEventHandler :
    IEventHandler<OrderCreatedEvent>,
    IEventHandler<OrderStateUpdatedEvent>
{
    
    private readonly IOrderRepository _orderRepository;

    public OrderEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(OrderCreatedEvent @event, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    public async Task Handle(OrderStateUpdatedEvent @event, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }
}