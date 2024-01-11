using Marte.EventSourcing.Core.Aggregate;
using Payment.Messages.Events;
using Payment.Messages.Models;

namespace Payment.Domain.Aggregates;

public class Repayment: AggregateRoot
{
    public Guid OrderId { get; protected set; }
    public RepaymentStatus Status { get; protected set; }
    public decimal Amount { get; protected set; }
    public string CardNumber { get; protected set; }
    public string CardOwner { get; protected set; }
    public string ExpirationDate { get; protected set; }
    public string CVV { get; protected set; }
    public DateTime RegDate { get; protected set; }

    public Repayment(Guid orderId, decimal amount, string cardNumber, string cardOwner, string expirationDate, string cvv)
    {
        Id = Guid.NewGuid();
        
        
        RaiseEvent(new RepaymentCreatedEvent()
        {
            Id = Id,
            OrderId = orderId,
            Amount = amount,
            CardNumber = cardNumber,
            CardOwner = cardOwner,
            CVV = cvv,
            ExpirationDate = expirationDate,
            RegDate = DateTime.Now,
            Status = RepaymentStatus.RepaymentCreated
        });
    }

    public Repayment()
    {
        
    }

    public void MarkAsProcessed()
    {
        RaiseEvent(new RepaymentProcessedEvent()
        {
            Id = Id,
            OrderId = OrderId
        });
    }
    
    public void Apply(RepaymentCreatedEvent @event)
    {
        Id = @event.Id;
        OrderId = @event.OrderId;
        Amount = @event.Amount;
        CardNumber = @event.CardNumber;
        CardOwner = @event.CardOwner;
        RegDate = @event.RegDate;
        CVV = @event.CVV;
        ExpirationDate = @event.ExpirationDate;
        Status = @event.Status;

        Version++;
    }
    
    public void Apply(RepaymentProcessedEvent @event)
    {
        Status = RepaymentStatus.RepaymentProcessed;

        Version++;
    }
    
    
}