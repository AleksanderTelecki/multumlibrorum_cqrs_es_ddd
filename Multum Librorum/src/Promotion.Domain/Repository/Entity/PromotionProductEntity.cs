using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Domain.Repository.Entity
{
    [Table("PromotionProduct", Schema = "Promotion")]
    public class PromotionProductEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        [Column("PromotionId")]
        public Guid PromotionEntityId { get; set; }
        [ForeignKey("PromotionEntityId")]
        public virtual PromotionEntity Promotion { get; set; }
    }
}
