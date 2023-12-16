using System.ComponentModel.DataAnnotations.Schema;

namespace Promotion.Domain.Repository.Entity
{
    [Table("PromotionProduct", Schema = "Promotion")]
    public class PromotionProductEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        [Column("PromotionId")]
        public Guid PromotionEntityId { get; set; }
        [ForeignKey("PromotionEntityId")]
        public virtual PromotionEntity Promotion { get; set; }

        public PromotionProductEntity()
        {
            
        }
    }
}
