using CQRS.Communication.Abstract;
using CQRS.Communication.Enums;
using CQRS.Core.Queries.Abstract;
using Product.Messages.Queries;
using Promotion.Domain.Repository;
using Promotion.Messages.Models;
using Promotion.Messages.Queries;

namespace Promotion.Domain.QueryHandlers;

public class PromotionQueryHandler:
    IQueryHandler<GetPromotionsQuery, List<PromotionModel>>,
    IQueryHandler<GetPromotionQuery, PromotionModel>
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IRestDispatcher _restDispatcher;

    public PromotionQueryHandler(IPromotionRepository promotionRepository, IRestDispatcher restDispatcher)
    {
        _promotionRepository = promotionRepository;
        _restDispatcher = restDispatcher;
    }


    public async Task<List<PromotionModel>> Handle(GetPromotionsQuery query, CancellationToken cancellationToken)
    {
        var promotions = await _promotionRepository.GetAllPromotions();

        List<PromotionModel> promotionModels = new();
        
        foreach (var promotion in promotions)
        {
            promotionModels.Add(new PromotionModel()
            {
                Id = promotion.Id,
                Description = promotion.Description,
                EndDate = promotion.EndDate,
                IsActive = promotion.IsActive,
                PromotionInPercentage = promotion.PromotionInPercentage,
                Regdate = promotion.Regdate
            });

            var promoModel = promotionModels.First(x => x.Id == promotion.Id);

            foreach (var productProm in promotion.Products)
            {
                var productInfo =
                    await _restDispatcher.DispatchQuery(new GetBookModelQuery() { ProductId = productProm.ProductId },
                        EndpointEnum.ProductEndpoint);
                
                promoModel.Products.Add(new PromotedProductModel() {Id = productInfo.Id, ProductName = productInfo.Title});
            }
            
        }

        return promotionModels;
    }

    public async Task<PromotionModel> Handle(GetPromotionQuery query, CancellationToken cancellationToken)
    {
        var promotion = await _promotionRepository.GetPromotion(query.PromotionId);
        
        var promotionModel = new PromotionModel()
        {
            Id = promotion.Id,
            Description = promotion.Description,
            EndDate = promotion.EndDate,
            IsActive = promotion.IsActive,
            PromotionInPercentage = promotion.PromotionInPercentage,
            Regdate = promotion.Regdate
        };
        
        foreach (var productProm in promotion.Products)
        {
            var productInfo =
                await _restDispatcher.DispatchQuery(new GetBookModelQuery() { ProductId = productProm.ProductId },
                    EndpointEnum.ProductEndpoint);
                
            promotionModel.Products.Add(new PromotedProductModel() {Id = productInfo.Id, ProductName = productInfo.Title});
        }

        return promotionModel;
    }
}