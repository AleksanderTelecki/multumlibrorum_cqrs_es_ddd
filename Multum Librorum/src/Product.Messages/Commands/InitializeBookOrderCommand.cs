using CQRS.Core.Commands.Abstract;

namespace Product.Messages.Commands;

public class InitializeBookOrderCommand: ICommand
{
    public List<(Guid productId, int quantity)> ProductsWithQuantity { get; set; }
}