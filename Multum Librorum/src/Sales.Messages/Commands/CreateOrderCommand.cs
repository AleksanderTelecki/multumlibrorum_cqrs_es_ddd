using CQRS.Core.Commands.Abstract;

namespace Sales.Messages.Commands;

public class CreateOrderCommand: ICommand
{
    public Guid ClientId { get; set; }
}