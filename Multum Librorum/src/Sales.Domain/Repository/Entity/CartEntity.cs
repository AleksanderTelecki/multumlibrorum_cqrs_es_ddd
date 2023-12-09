using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Repository.Entity
{
    [Table("Cart", Schema = "Sales")]
    public class CartEntity
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        public virtual ICollection<CartItemEntity> Items { get; set; } = new List<CartItemEntity>();

        public CartEntity()
        {
            
        }

        public CartEntity(Guid id, Guid clientId)
        {
            Id = id;
            ClientId = clientId;
            Items = new List<CartItemEntity>();
        }
    }
}
