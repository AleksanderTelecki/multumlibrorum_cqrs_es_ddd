using Marte.EventSourcing.Core.Aggregate;
using Product.Messages.Events;
using Product.Messages.Events.Models;

namespace Product.Domain.Aggregates
{
    public class Book: AggregateRoot
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Description { get; private set; }
        public int PageCount { get; private set; }
        public DateTime RegDate { get; private set; }
        public decimal Price { get; private set; }
        public decimal? PromotedPrice { get; private set; }
        public int Quantity { get; private set; }
        public bool IsHidden { get; private set; }
        public List<Comment> Comments { get; private set; } = new List<Comment>();

        public Book()
        {
            
        }

        public Book(string title, string author, string description, decimal price, int quantity, int pageCount)
        {
            Id = Guid.NewGuid();

            RaiseEvent(new BookAddedEvent
            {
                Id = Id,
                Title = title,
                Author = author,
                Description = description,
                Price = price,
                PageCount = pageCount,
                RegDate = DateTime.Now,
                Quantity = quantity
            });
        }

        public void UpdateDetails(string title, string author, string description, int pageCount)
        {
            RaiseEvent(new BookDetailsUpdatedEvent
            {
                Id = Id,
                Title = title,
                Author = author,
                Description = description,
                PageCount = pageCount
            });
        }

        public void UpdatePrice(decimal price)
        {
            RaiseEvent(new BookPriceUpdatedEvent
            {
                Id = Id,
                Price = price,
            });
        }

        public void UpdateQuantity(int quantity)
        {
            RaiseEvent(new BookQuantityUpdatedEvent
            {
                Id = Id,
                Quantity = quantity,
            });
        }

        public void MarkAsHidden()
        {
            RaiseEvent(new BookMarkedAsHiddenEvent
            {
                Id = Id
            });
        }

        public void MarkAsUnHidden()
        {
            RaiseEvent(new BookMarkedAsUnHiddenEvent
            {
                Id = Id
            });
        }

        public void AddComment(Comment comment)
        {
            RaiseEvent(new BookCommentAddedEvent
            {
                Id = Id,
                Comment = comment
            });
        }

        public void RemoveComment(Comment comment)
        {
            RaiseEvent(new BookCommentRemovedEvent
            {
                Id = Id,
                Comment = comment
            });
        }

        public void SetPromotedPrice(decimal? promotedPrice)
        {
            RaiseEvent(new BookPromotedPriceChangedEvent
            {
                Id = Id,
                PromotedPrice = promotedPrice
            });
        }

        public void Apply(BookAddedEvent @event)
        {
            Id = @event.Id;
            Title = @event.Title;
            Author = @event.Author;
            Description = @event.Description;
            PageCount = @event.PageCount;
            RegDate = @event.RegDate;
            Price = @event.Price;
            Quantity = @event.Quantity;

            Version++;
        }

        public void Apply(BookDetailsUpdatedEvent @event)
        {
            Title = @event.Title;
            Author = @event.Author;
            Description = @event.Description;
            PageCount = @event.PageCount;

            Version++;
        }


        public void Apply(BookPriceUpdatedEvent @event)
        {
            Price = @event.Price;

            Version++;
        }

        public void Apply(BookQuantityUpdatedEvent @event)
        {
            Quantity = @event.Quantity;

            Version++;
        }

        public void Apply(BookMarkedAsHiddenEvent @event)
        {
            IsHidden = true;

            Version++;
        }

        public void Apply(BookMarkedAsUnHiddenEvent @event)
        {
            IsHidden = false;

            Version++;
        }
        
        
        public void Apply(BookCommentAddedEvent @event)
        {
            Comments.Add(@event.Comment);

            Version++;
        }

        public void Apply(BookCommentRemovedEvent @event)
        {
            Comments.Remove(@event.Comment);

            Version++;
        }

        public void Apply(BookPromotedPriceChangedEvent @event)
        {
            PromotedPrice = @event.PromotedPrice;

            Version++;
        }
    }

}
