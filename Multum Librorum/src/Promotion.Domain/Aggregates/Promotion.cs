using Marte.EventSourcing.Core.Aggregate;
using Promotion.Messages.Events;

namespace Promotion.Domain.Aggregates
{
    public class Promotion: AggregateRoot
    {
        public string Description { get; private set; }
        public decimal PromotionInPercentage { get; private set; }
        public List<Guid> Products { get; private set; } = new List<Guid>();
        public bool IsActive { get; private set; }
        public DateTime Regdate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Promotion()
        {
            
        }

        public Promotion(string description, decimal promotionInPercentage, List<Guid> products, DateTime endDate)
        {
            Id = Guid.NewGuid();

            RaiseEvent(new PromotionAddedEvent
            {
                Id = Id,
                Description = description,
                Regdate = DateTime.Now,
                EndDate = endDate,
                Products = products,
                PromotionInPercentage = promotionInPercentage
            });
        }

        public void Edit(string description, decimal promotionInPercentage, List<Guid> productsId, DateTime endDate)
        {
            RaiseEvent(new PromotionEditedEvent
            {
                Id = Id,
                Description = description,
                NewProducts = productsId,
                PreviousProducts = Products,
                PromotionInPercentage = promotionInPercentage,
                EndDate = endDate,
            });
        }

        public void EndPromotion()
        {
            RaiseEvent(new PromotionEndedEvent
            {
                Id = Id,
                Products = Products
            });
        }

        public void Apply(PromotionAddedEvent @event)
        {
            Id = @event.Id;
            Description = @event.Description;
            Regdate = @event.Regdate;
            PromotionInPercentage = @event.PromotionInPercentage;
            IsActive = true;
            Products = @event.Products;
            EndDate = @event.EndDate;

            Version++;
        }

        public void Apply(PromotionEditedEvent @event)
        {
            Description = @event.Description;
            PromotionInPercentage = @event.PromotionInPercentage;
            Products = @event.NewProducts;
            EndDate = @event.EndDate;

            Version++;
        }

        public void Apply(PromotionEndedEvent @event)
        {
            IsActive = false;
            Version++;
        }


    }
}
