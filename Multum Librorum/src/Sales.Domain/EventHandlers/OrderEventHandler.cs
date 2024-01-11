using CQRS.Core.Events;
using CQRS.Core.Events.Abstract;
using Sales.Domain.Repository;
using Sales.Messages.Enums;
using Sales.Messages.Events;

namespace Sales.Domain.EventHandlers;

public class OrderEventHandler :
    IEventHandler<OrderCreatedEvent>,
    IEventHandler<OrderStateUpdatedEvent>,
    IEventHandler<OrderProductsAddedEvent>,
    IEventHandler<OrderRealizedEvent>,
    IEventHandler<OrderPaidEvent>
{
    
    private readonly IOrderRepository _orderRepository;

    public OrderEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(OrderCreatedEvent @event, CancellationToken cancellation)
    {
        await _orderRepository.CreateOrderForClient(@event.Id, @event.ClientId);
    }

    public async Task Handle(OrderStateUpdatedEvent @event, CancellationToken cancellation)
    {
        await _orderRepository.ChangeOrderState(@event.Id, @event.NewOrderState);
    }

    public async Task Handle(OrderProductsAddedEvent @event, CancellationToken cancellation)
    { 
        await _orderRepository.AddProductsToOrder(@event.Id, @event.OrderItems);
    }

    public async Task Handle(OrderRealizedEvent @event, CancellationToken cancellation)
    {
        await _orderRepository.ChangeOrderState(@event.Id, OrderState.Realized);
    }

    public async Task Handle(OrderPaidEvent @event, CancellationToken cancellation)
    {
        await _orderRepository.ChangeOrderState(@event.Id, OrderState.Paid);
    }
}