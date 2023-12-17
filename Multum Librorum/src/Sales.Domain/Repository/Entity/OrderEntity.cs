using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sales.Messages.Enums;

namespace Sales.Domain.Repository.Entity;

[Table("Orders", Schema = "Sale")]
public class OrderEntity
{
    public Guid Id { get; set; }

    [Required]
    public Guid ClientId { get; set; }
    
    public OrderState State { get; set; }
    
    public virtual ICollection<OrderItemEntity> Items { get; set; } = new List<OrderItemEntity>();

    public OrderEntity()
    {
        
    }
    
    public OrderEntity(CartEntity cartEntity)
    {
        Id = Guid.NewGuid();
        ClientId = cartEntity.ClientId;
        State = OrderState.OrderCreated;
    }
}