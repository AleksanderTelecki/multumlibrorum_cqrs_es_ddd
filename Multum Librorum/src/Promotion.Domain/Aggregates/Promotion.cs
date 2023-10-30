using Marte.EventSourcing.Core.Aggregate;
using Promotion.Messages.Events;

namespace Promotion.Domain.Aggregates
{
    public class Promotion: AggregateRoot
    {
        public string Description { get; private set; }
        public decimal PromotionInPercentage { get; private set; }
        public List<Guid> ProductIds { get; private set; }
        public DateTime Regdate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Promotion()
        {
            
        }

        public Promotion(string description, decimal promotionInPercentage, List<Guid> productsId, DateTime endDate)
        {
            Id = Guid.NewGuid();

            RaiseEvent(new PromotionAddedEvent
            {
                Id = Id,
                Description = description,
                Regdate = DateTime.Now,
                EndDate = endDate,
                ProductIds = productsId,
                PromotionInPercentage = promotionInPercentage
            });
        }

        public void Change(string description, decimal promotionInPercentage, List<Guid> productsId, DateTime endDate)
        {
            RaiseEvent(new PromotionChangedEvent
            {
                Id = Id,
                Description = description,
                ProductIds = productsId,
                PromotionInPercentage = promotionInPercentage,
                EndDate = endDate,
            });
        }

        public void EndPromotion()
        {
            RaiseEvent(new PromotionEndedEvent
            {
                Id = Id,
                ProductIds = ProductIds
            });
        }

        public void Apply(PromotionAddedEvent @event)
        {
            Id = @event.Id;
            Description = @event.Description;
            Regdate = @event.Regdate;
            PromotionInPercentage = @event.PromotionInPercentage;
            ProductIds = @event.ProductIds;
            EndDate = @event.EndDate;

            Version++;
        }

        public void Apply(PromotionChangedEvent @event)
        {
            Description = @event.Description;
            PromotionInPercentage = @event.PromotionInPercentage;
            ProductIds = @event.ProductIds;
            EndDate = @event.EndDate;

            Version++;
        }

        public void Apply(PromotionEndedEvent @event)
        {
            Version++;
        }


    }
}
