using CQRS.Core.Events;

namespace Payment.Messages.Events;

public class RepaymentProcessedEvent: Event
{
    public Guid OrderId { get; set; }
}