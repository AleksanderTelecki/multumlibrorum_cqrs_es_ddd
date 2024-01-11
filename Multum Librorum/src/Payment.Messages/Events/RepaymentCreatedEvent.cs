using CQRS.Core.Events;
using Payment.Messages.Models;

namespace Payment.Messages.Events;

public class RepaymentCreatedEvent: Event
{
    public Guid OrderId { get; set; }
    public RepaymentStatus Status { get; set; }
    public decimal Amount { get; set; }
    public string CardNumber { get; set; }
    public string CardOwner { get; set; }
    public string ExpirationDate { get; set; }
    public string CVV { get; set; }
    public DateTime RegDate { get; set; }
}