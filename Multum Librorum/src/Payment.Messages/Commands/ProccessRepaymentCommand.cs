using CQRS.Core.Commands.Abstract;

namespace Payment.Messages.Commands;

public class ProccessRepaymentCommand: ICommand
{
    public Guid RepaymentId { get; set; }
}