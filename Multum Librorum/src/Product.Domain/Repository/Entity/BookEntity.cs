using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Repository.Entity
{
    [Table("Books", Schema = "Product")]
    public class BookEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public int PageCount { get; set; }
        public DateTime RegDate { get; set; }

        public virtual List<PromotionEntity> Promotions { get; set; }
        public virtual List<CommentEntity> Comments { get; set; }

        public BookEntity()
        {
            
        }

    }
}
