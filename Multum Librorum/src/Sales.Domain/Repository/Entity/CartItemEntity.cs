using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Repository.Entity
{
    [Table("CartItem", Schema = "Sales")]
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
