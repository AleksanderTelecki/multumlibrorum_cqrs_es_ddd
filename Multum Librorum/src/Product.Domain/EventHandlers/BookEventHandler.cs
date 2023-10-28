using CQRS.Core.Events;
using CQRS.Core.Events.Abstract;
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

        public async Task Handle(BookAddedEvent @event, CancellationToken cancellationToken)
        {
            var bookEntity = new BookEntity { 
                Id = @event.Id, 
                Author= @event.Author,
                Title= @event.Title,
                Description= @event.Description,
                PageCount= @event.PageCount,
                RegDate= @event.RegDate
            };

            await _productRepository.CreateBook(bookEntity);
        }
    }
}
