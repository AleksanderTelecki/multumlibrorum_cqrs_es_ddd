using CQRS.Core.Events;
using Product.Domain.Repository;
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
        private readonly IProductRepository _productRepository;

        public BookEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(BookAddedEvent notification, CancellationToken cancellationToken)
        {
            var bookEntity = new BookEntity { 
                Id = notification.Id, 
                Author=notification.Author,
                Title=notification.Title,
                Description=notification.Description,
                PageCount=notification.PageCount,
                RegDate=notification.RegDate
            };

            await _productRepository.CreateBook(bookEntity);
        }
    }
}
