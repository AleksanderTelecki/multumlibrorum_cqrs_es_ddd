using CQRS.Core.Queries.Abstract;
using Promotion.Messages.Models;

namespace Promotion.Messages.Queries;

public class GetPromotionsQuery: IQuery<List<PromotionModel>>
{
    
}