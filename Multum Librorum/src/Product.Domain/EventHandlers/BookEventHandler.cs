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
    public class BookEventHandler : 
        IEventHandler<BookAddedEvent>,
        IEventHandler<BookDetailsUpdatedEvent>,
        IEventHandler<BookPriceUpdatedEvent>,
        IEventHandler<BookQuantityUpdatedEvent>,
        IEventHandler<BookMarkedAsHiddenEvent>,
        IEventHandler<BookMarkedAsUnHiddenEvent>,
        IEventHandler<BookCommentAddedEvent>,
        IEventHandler<BookCommentRemovedEvent>

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
                Quantity= @event.Quantity,
                Price= @event.Price,
                PageCount= @event.PageCount,
                RegDate= @event.RegDate
            };

            await _productRepository.CreateBook(bookEntity);
        }

        public async Task Handle(BookDetailsUpdatedEvent @event, CancellationToken cancellation)
        {
            var bookEntity = await _productRepository.GetBook(@event.Id);

            bookEntity.Author = @event.Author;
            bookEntity.Title = @event.Title;
            bookEntity.Description = @event.Description;
            bookEntity.PageCount = @event.PageCount;

            await _productRepository.UpdateBook(bookEntity);
        }

        public async Task Handle(BookPriceUpdatedEvent @event, CancellationToken cancellation)
        {
            var bookEntity = await _productRepository.GetBook(@event.Id);

            bookEntity.Price = @event.Price;

            await _productRepository.UpdateBook(bookEntity);
        }

        public async Task Handle(BookQuantityUpdatedEvent @event, CancellationToken cancellation)
        {
            var bookEntity = await _productRepository.GetBook(@event.Id);

            bookEntity.Quantity = @event.Quantity;

            await _productRepository.UpdateBook(bookEntity);
        }

        public async Task Handle(BookMarkedAsHiddenEvent @event, CancellationToken cancellation)
        {
            var bookEntity = await _productRepository.GetBook(@event.Id);

            bookEntity.IsHidden = true;

            await _productRepository.UpdateBook(bookEntity);
        }

        public async Task Handle(BookMarkedAsUnHiddenEvent @event, CancellationToken cancellation)
        {
            var bookEntity = await _productRepository.GetBook(@event.Id);

            bookEntity.IsHidden = false;

            await _productRepository.UpdateBook(bookEntity);
        }

        public async Task Handle(BookCommentAddedEvent @event, CancellationToken cancellation)
        {
            var bookEntity = await _productRepository.GetBook(@event.Id);

            bookEntity.Comments.Add(new CommentEntity(@event.Comment));

            await _productRepository.UpdateBook(bookEntity);
        }

        public async Task Handle(BookCommentRemovedEvent @event, CancellationToken cancellation)
        {
            var bookEntity = await _productRepository.GetBook(@event.Id);

            bookEntity.Comments.Remove(new CommentEntity(@event.Comment));

            await _productRepository.UpdateBook(bookEntity);
        }
    }
}
