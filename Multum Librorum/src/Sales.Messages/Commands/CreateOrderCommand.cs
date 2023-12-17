using CQRS.Core.Commands.Abstract;

namespace Sales.Messages.Commands;

public class CreateOrderCommand: Command
{
    public Guid CartId { get; set; }
}