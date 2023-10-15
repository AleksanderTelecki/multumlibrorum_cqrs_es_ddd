using CQRS.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Product.Domain.Aggregates.Additionals;
using Product.Domain.Repository.Entity;
using Product.Messages.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Aggregates
{
    public class Book: AggregateRoot
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public decimal Price { get; private set; }
        public int PageCount { get; private set; }
        public DateTime RegDate { get; private set; }
        public List<Comment> Comments { get; private set; } = new List<Comment>();

        public Book()
        {
            
        }

        public Book(string title, string author, decimal price, int pageCount)
        {
            Id = Guid.NewGuid();

            RaiseEvent(new BookAddedEvent
            {
                Id = Id,
                Title = title,
                Author = author,
                Price = price,
                PageCount = pageCount,
                RegDate = DateTime.Now
            });
        }

        public void Apply(BookAddedEvent @event)
        {
            Id = @event.Id;
            Title = @event.Title;
            Author = @event.Author;
            Price = @event.Price;
            PageCount = @event.PageCount;
            RegDate = @event.RegDate;

            Version++;
        }
    }

}
