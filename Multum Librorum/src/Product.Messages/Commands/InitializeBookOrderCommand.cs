using CQRS.Core.Commands.Abstract;

namespace Product.Messages.Commands;

public class InitializeBookOrderCommand: ICommand
{
    public Dictionary<Guid, int> ProductsWithQuantity { get; set; }
}