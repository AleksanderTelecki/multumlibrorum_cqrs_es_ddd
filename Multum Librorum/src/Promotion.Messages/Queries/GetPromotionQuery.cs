using CQRS.Core.Queries.Abstract;
using Promotion.Messages.Models;

namespace Promotion.Messages.Queries;

public class GetPromotionQuery: IQuery<PromotionModel>
{
    public Guid PromotionId { get; set; }
}