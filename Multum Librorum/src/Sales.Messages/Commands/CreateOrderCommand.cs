using CQRS.Core.Commands.Abstract;

namespace Sales.Messages.Commands;

public class CreateOrderCommand: ICommand
{
    public Guid CartId { get; set; }
}