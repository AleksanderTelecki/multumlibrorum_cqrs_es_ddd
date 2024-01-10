namespace Sales.Messages.Models;

public class OrderItemModel
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }
    
    public string ProductName { get; set; }
    
    public decimal ProductPrice { get; set; }

    public int Quantity { get; set; }
}