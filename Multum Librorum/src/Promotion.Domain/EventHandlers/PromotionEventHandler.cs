using CQRS.Core.Events.Abstract;
using Promotion.Domain.Repository;
using Promotion.Domain.Repository.Entity;
using Promotion.Messages.Events;

namespace Promotion.Domain.EventHandlers
{
    public class PromotionEventHandler :
        IEventHandler<PromotionAddedEvent>,
        IEventHandler<PromotionEditedEvent>,
        IEventHandler<PromotionEndedEvent>
    {

        private readonly IPromotionRepository _promotionRepository;
        public PromotionEventHandler(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task Handle(PromotionAddedEvent @event, CancellationToken cancellation)
        {
            var promotionEntity = new PromotionEntity
            {
                Id = @event.Id,
                PromotionInPercentage = @event.PromotionInPercentage,
                Description = @event.Description,
                EndDate = @event.EndDate,
                Regdate = @event.Regdate
            };

            foreach (var productId in @event.Products)
            {
                promotionEntity.Products.Add(new PromotionProductEntity { Id = Guid.NewGuid(), ProductId = productId });
            }

            await _promotionRepository.CreatePromotion(promotionEntity);
        }

        public async Task Handle(PromotionEditedEvent @event, CancellationToken cancellation)
        {
            var promotionEntity = await _promotionRepository.GetPromotion(@event.Id);

            promotionEntity.Description = @event.Description;
            promotionEntity.EndDate = @event.EndDate;
            promotionEntity.PromotionInPercentage = @event.PromotionInPercentage;
            
            promotionEntity.Products.Clear();
            foreach (var productId in @event.NewProducts)
            {
                promotionEntity.Products.Add(new PromotionProductEntity { Id = Guid.NewGuid(), ProductId = productId });
            }

            await _promotionRepository.UpdatePromotion(promotionEntity);
        }

        public async Task Handle(PromotionEndedEvent @event, CancellationToken cancellation)
        {
            var promotionEntity = await _promotionRepository.GetPromotion(@event.Id);
            promotionEntity.IsActive = false;
            await _promotionRepository.UpdatePromotion(promotionEntity);
        }
    }
}
