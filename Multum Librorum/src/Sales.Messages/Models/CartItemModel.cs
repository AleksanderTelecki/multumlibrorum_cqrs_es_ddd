namespace Sales.Messages.Models;

public class CartItemModel
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }
    
    public string ProductName { get; set; }
    
    public decimal ProductPrice { get; set; }

    public int Quantity { get; set; }
    
    public bool IsOnStock { get; set; }

    public CartItemModel(Guid id, Guid productId, string productName, decimal productPrice, int quantity, bool isOnStock)
    {
        Id = id;
        ProductId = productId;
        ProductName = productName;
        ProductPrice = productPrice;
        Quantity = quantity;
        IsOnStock = isOnStock;
    }
}