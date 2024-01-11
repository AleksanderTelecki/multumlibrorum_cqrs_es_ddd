using CQRS.Core.Commands.Abstract;
using Marte.EventSourcing.Core.Abstract;
using Payment.Domain.Aggregates;
using Payment.Messages.Commands;

namespace Payment.Domain.CommandHandlers;

public class RepaymentCommandHandler:
    ICommandHandler<CreateRepaymentToOrderCommand>,
    ICommandHandler<ProccessRepaymentCommand>
{
    
    private readonly IAggregateRepository _aggregateRepository;

    public RepaymentCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public async Task Handle(CreateRepaymentToOrderCommand command, CancellationToken cancellation)
    {
        var repayment = new Repayment(command.OrderId, command.Amount, command.CardNumber, command.CardOwner,
            command.ExpirationDate, command.CVV);
        
        await _aggregateRepository.StoreAsync(repayment);
    }

    public async Task Handle(ProccessRepaymentCommand command, CancellationToken cancellation)
    {
        var repayment = await _aggregateRepository.LoadAsync<Repayment>(command.RepaymentId);
        
        // TODO: RepaymentLogic
        
        repayment.MarkAsProcessed();
        
        await _aggregateRepository.StoreAsync(repayment);
    }
}