using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Domain.Repository.Entity
{
    [Table("Promotions", Schema = "Promotion")]
    public class PromotionEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal PromotionInPercentage { get; set; }
        public virtual ICollection<PromotionProductEntity> Products { get; set; } = new List<PromotionProductEntity>();
        public bool IsActive { get; set; } = true;
        public DateTime Regdate { get;set; }
        public DateTime EndDate { get;set; }

        public PromotionEntity()
        {
            
        }
    }
}
