using CQRS.Core.Commands.Abstract;
using CQRS.Core.Events;
using CQRS.Core.Events.Abstract;
using Payment.Domain.Repository;
using Payment.Domain.Repository.Entity;
using Payment.Messages.Commands;
using Payment.Messages.Events;
using Payment.Messages.Models;

namespace Payment.Domain.EventHandlers;

public class RepaymentEventHandler: 
    IEventHandler<RepaymentCreatedEvent>,
    IEventHandler<RepaymentProcessedEvent>
    
{
    private readonly IRepaymentRepository _repaymentRepository;
    private readonly ICommandDispatcher _commandDispatcher;

    public RepaymentEventHandler(IRepaymentRepository repaymentRepository, ICommandDispatcher commandDispatcher)
    {
        _repaymentRepository = repaymentRepository;
        _commandDispatcher = commandDispatcher;
    }

    public async Task Handle(RepaymentCreatedEvent @event, CancellationToken cancellation)
    {
        var repaymentEntity = new RepaymentEntity(@event.Id, @event.OrderId, @event.Amount, @event.CardNumber,
            @event.CardOwner, @event.ExpirationDate, @event.CVV, @event.RegDate, @event.Status);

        await _repaymentRepository.CreateNewRepayment(repaymentEntity);
        
        await _commandDispatcher.Dispatch(new ProccessRepaymentCommand() { RepaymentId = @event.Id });
    }

    public async Task Handle(RepaymentProcessedEvent @event, CancellationToken cancellation)
    {
        await _repaymentRepository.UpdateStatus(@event.Id, RepaymentStatus.RepaymentProcessed);
    }
}