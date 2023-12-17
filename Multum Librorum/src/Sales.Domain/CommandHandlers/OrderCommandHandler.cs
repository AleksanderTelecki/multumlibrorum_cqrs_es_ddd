using Client.Messages.Queries;
using CQRS.Communication.Abstract;
using CQRS.Communication.Enums;
using CQRS.Core.Commands.Abstract;
using Marte.EventSourcing.Core.Abstract;
using Sales.Messages.Commands;

namespace Sales.Domain.CommandHandlers;

public class OrderCommandHandler: 
    ICommandHandler<CreateOrderCommand>
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IRestDispatcher _restDispatcher;

    public OrderCommandHandler(IAggregateRepository aggregateRepository, IRestDispatcher restDispatcher)
    {
        _aggregateRepository = aggregateRepository;
        _restDispatcher = restDispatcher;
    }


    public async Task Handle(CreateOrderCommand command, CancellationToken cancellation)
    {

        var result = await _restDispatcher.DispatchQuery(new GetClientByEmailQuery { Email = "example4@email.com" }, EndpointEnum.ClientEndpoint);

        var x = 6;
        throw new NotImplementedException();
    }
}