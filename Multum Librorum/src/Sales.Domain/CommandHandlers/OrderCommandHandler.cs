using CQRS.Communication.Abstract;
using CQRS.Communication.Enums;
using CQRS.Core.Commands.Abstract;
using Marte.EventSourcing.Core.Abstract;
using Product.Messages.Commands;
using Sales.Domain.Aggregates;
using Sales.Messages.Commands;

namespace Sales.Domain.CommandHandlers;

public class OrderCommandHandler: 
    ICommandHandler<CreateOrderCommand>
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IRestDispatcher _restDispatcher;

    private readonly object _lockObj = new object();
    
    public OrderCommandHandler(IAggregateRepository aggregateRepository, IRestDispatcher restDispatcher)
    {
        _aggregateRepository = aggregateRepository;
        _restDispatcher = restDispatcher;
    }


    public async Task Handle(CreateOrderCommand command, CancellationToken cancellation)
    {
        var cart = await _aggregateRepository.LoadAsync<Cart>(command.CartId);
        
        await _restDispatcher.DispatchCommand(
            new InitializeBookOrderCommand { ProductsWithQuantity = cart.Items.ToDictionary(x => x.ProductId, y => y.Quantity)}, EndpointEnum.ProductEndpoint);
        
    }
}