using CQRS.Core.Commands.Abstract;
using CQRS.Core.Events.Abstract;
using Payment.Messages.Events;
using Sales.Messages.Commands;
using Sales.Messages.Enums;

namespace Sales.Domain.EventHandlers;

public class PaymentEventHandler: IEventHandler<RepaymentProcessedEvent>
{

    private readonly ICommandDispatcher _commandDispatcher;

    public PaymentEventHandler(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    public async Task Handle(RepaymentProcessedEvent @event, CancellationToken cancellation)
    {
        await _commandDispatcher.Dispatch(new ChangeOrderStateCommand()
            { OrderState = OrderState.Paid, OrderId = @event.OrderId });
    }
}