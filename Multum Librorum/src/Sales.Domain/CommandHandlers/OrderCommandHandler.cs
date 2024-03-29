﻿using CQRS.Communication.Abstract;
using CQRS.Communication.Enums;
using CQRS.Core.Commands.Abstract;
using Marte.EventSourcing.Core.Abstract;
using Product.Messages.Commands;
using Sales.Domain.Aggregates;
using Sales.Messages.Commands;
using Sales.Messages.Enums;
using Sales.Messages.Models;

namespace Sales.Domain.CommandHandlers;

public class OrderCommandHandler: 
    ICommandHandler<CreateOrderCommand>,
    ICommandHandler<ChangeOrderStateCommand>
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IRestDispatcher _restDispatcher;

    private readonly object _lockObject = new object();
    
    public OrderCommandHandler(IAggregateRepository aggregateRepository, IRestDispatcher restDispatcher)
    {
        _aggregateRepository = aggregateRepository;
        _restDispatcher = restDispatcher;
    }


    public async Task Handle(CreateOrderCommand command, CancellationToken cancellation)
    {
        lock (_lockObject)
        {
            var cart = _aggregateRepository.LoadAsync<Cart>(command.CartId).GetAwaiter().GetResult();
            
            _restDispatcher.DispatchCommand(
                new InitializeBookOrderCommand
                    { ProductsWithQuantity = cart.Items.ToDictionary(x => x.ProductId, y => y.Quantity) },
                EndpointEnum.ProductEndpoint).GetAwaiter().GetResult();

            var order = new Order(cart.ClientId, command.OrderId);
            var orderedProducts = cart.Items.Select(x => new OrderItem(Guid.NewGuid(), x.ProductId, x.Quantity));
            
            order.AddProductsToOrder(orderedProducts.ToList());
            order.MarkAsPlaced();
            
            cart.Clear();
            
            _aggregateRepository.StoreAsync(order).GetAwaiter().GetResult();
            _aggregateRepository.StoreAsync(cart).GetAwaiter().GetResult();
        }
    }

    public async Task Handle(ChangeOrderStateCommand command, CancellationToken cancellation)
    {
        var order = await _aggregateRepository.LoadAsync<Order>(command.OrderId);

        if(command.OrderState == OrderState.Paid)
            order.MarkAsPaid();
        
        if(command.OrderState == OrderState.Realized)
            order.MarkAsRealized();

        await _aggregateRepository.StoreAsync(order);
    }
}