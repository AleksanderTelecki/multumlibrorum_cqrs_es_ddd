using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.Domain.Repository.Entity
{
    [Table("CartItems", Schema = "Sale")]
    public class CartItemEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        [Column("CartId")]
        public Guid CartEntityId { get; set; }

        [ForeignKey("CartEntityId")]
        public virtual CartEntity Cart { get; set; }

        public CartItemEntity()
        {
            
        }

        public CartItemEntity(Guid id, Guid productId, int quantity)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
