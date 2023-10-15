using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Repository.Entity
{
    [Table("Promotions", Schema = "Product")]
    public class PromotionEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<BookEntity> PromotedBooks { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }

        public PromotionEntity()
        {
            
        }

    }
}
