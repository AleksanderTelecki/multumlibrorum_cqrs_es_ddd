namespace Promotion.Messages.Models;

public class PromotionModel
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public decimal PromotionInPercentage { get; set; }
    public virtual List<PromotedProductModel> Products { get; set; } = new List<PromotedProductModel>();
    public bool IsActive { get; set; }
    public DateTime Regdate { get;set; }
    public DateTime EndDate { get;set; }
}