using CQRS.Core.Commands.Abstract;
using Sales.Messages.Enums;

namespace Sales.Messages.Commands;

public class ChangeOrderStateCommand: ICommand
{
    public Guid OrderId { get; set; }
    public OrderState OrderState { get; set; }
}