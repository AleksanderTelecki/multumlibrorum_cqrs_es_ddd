using CQRS.Core.Commands.Abstract;
using Payment.Messages.Models;

namespace Payment.Messages.Commands;

public class CreateRepaymentToOrderCommand: ICommand
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string CardNumber { get; set; }
    public string CardOwner { get; set; }
    public string ExpirationDate { get; set; }
    public string CVV { get; set; }
}