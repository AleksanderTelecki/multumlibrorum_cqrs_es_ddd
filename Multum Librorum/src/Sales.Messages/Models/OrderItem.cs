namespace Sales.Messages.Models;

public class OrderItem
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public OrderItem()
    {

    }

    public OrderItem(Guid id, Guid productId, int quantity)
    {
        Id = id;
        ProductId = productId;
        Quantity = quantity;
    }
}