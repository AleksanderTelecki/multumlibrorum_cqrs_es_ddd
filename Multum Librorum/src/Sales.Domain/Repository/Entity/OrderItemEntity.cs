using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.Domain.Repository.Entity;

[Table("OrderItems", Schema = "Sale")]
public class OrderItemEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    [Column("OrderId")]
    public Guid OrderEntityId { get; set; }

    [ForeignKey("OrderEntityId")]
    public virtual OrderEntity Order { get; set; }
    
    public OrderItemEntity()
    {
            
    }
    
    public OrderItemEntity(CartItemEntity cartItemEntity)
    {
        Id = Guid.NewGuid();
        ProductId = cartItemEntity.ProductId;
        Quantity = cartItemEntity.Quantity;
    }
}