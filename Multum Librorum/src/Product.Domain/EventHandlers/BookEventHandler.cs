using CQRS.Core.Events;
using Product.Domain.Repository.Entity;
using Product.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.EventHandlers
{
    public class BookEventHandler : IEventHandler<BookAddedEvent>
    {
        private readonly ProductDomainDataContext _productDomainDataContext;

        public BookEventHandler(ProductDomainDataContext productDomainDataContext)
        {
            _productDomainDataContext = productDomainDataContext;
        }

        public async Task Handle(BookAddedEvent notification, CancellationToken cancellationToken)
        {
            var bookEntity = new BookEntity { 
                Id = notification.Id, 
                Author=notification.Author,
                Title=notification.Title,
                PageCount=notification.PageCount,
                Price=notification.Price,
                RegDate=notification.RegDate
            };

            _productDomainDataContext.Add(bookEntity);
            await _productDomainDataContext.SaveChangesAsync();
        }
    }
}
