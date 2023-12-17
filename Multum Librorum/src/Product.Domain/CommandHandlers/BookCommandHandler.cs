using CQRS.Core.Commands.Abstract;
using Marte.EventSourcing.Core.Abstract;
using Product.Domain.Aggregates;
using Product.Domain.Repository;
using Product.Messages.Commands;

namespace Product.Domain.CommandHandlers
{
    public class BookCommandHandler :
        ICommandHandler<AddBookCommand>,
        ICommandHandler<UpdateBookDetailsCommand>,
        ICommandHandler<UpdateBookPriceCommand>,
        ICommandHandler<UpdateBookQuantityCommand>,
        ICommandHandler<HideBookCommand>,
        ICommandHandler<UnHideBookCommand>,
        ICommandHandler<AddCommentToBookCommand>,
        ICommandHandler<RemoveBookCommentCommand>,
        ICommandHandler<AddBookPromotionCommand>,
        ICommandHandler<RemoveBookPromotionCommand>
    {
        private readonly IAggregateRepository _aggregateRepository;

        public BookCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public async Task Handle(AddBookCommand command, CancellationToken cancellationToken)
        {
            var book = new Book(command.Title, command.Author, command.Description, command.Price, command.Quantity, command.PageCount);
            await _aggregateRepository.StoreAsync(book);
        }

        public async Task Handle(UpdateBookDetailsCommand command, CancellationToken cancellation)
        {
            var book = await _aggregateRepository.LoadAsync<Book>(command.Id);
            book.UpdateDetails(command.Title, command.Author, command.Description, command.PageCount);

            await _aggregateRepository.StoreAsync(book);
        }

        public async Task Handle(UpdateBookPriceCommand command, CancellationToken cancellation)
        {
            var book = await _aggregateRepository.LoadAsync<Book>(command.Id);
            book.UpdatePrice(command.Price);

            await _aggregateRepository.StoreAsync(book);
        }

        public async Task Handle(UpdateBookQuantityCommand command, CancellationToken cancellation)
        {
            var book = await _aggregateRepository.LoadAsync<Book>(command.Id);
            book.UpdateQuantity(command.Quantity);

            await _aggregateRepository.StoreAsync(book);
        }

        public async Task Handle(HideBookCommand command, CancellationToken cancellation)
        {
            var book = await _aggregateRepository.LoadAsync<Book>(command.Id);
            book.MarkAsHidden();

            await _aggregateRepository.StoreAsync(book);
        }

        public async Task Handle(UnHideBookCommand command, CancellationToken cancellation)
        {
            var book = await _aggregateRepository.LoadAsync<Book>(command.Id);
            book.MarkAsUnHidden();

            await _aggregateRepository.StoreAsync(book);
        }

        public async Task Handle(AddCommentToBookCommand command, CancellationToken cancellation)
        {
            var book = await _aggregateRepository.LoadAsync<Book>(command.Id);
            book.AddComment(command.Comment);

            await _aggregateRepository.StoreAsync(book);
        }

        public async Task Handle(RemoveBookCommentCommand command, CancellationToken cancellation)
        {
            var book = await _aggregateRepository.LoadAsync<Book>(command.Id);
            book.RemoveComment(command.Comment);

            await _aggregateRepository.StoreAsync(book);
        }

        public async Task Handle(AddBookPromotionCommand command, CancellationToken cancellation)
        {
            var book = await _aggregateRepository.LoadAsync<Book>(command.Id);
            book.SetPromotedPrice(book.Price * command.PromotedPercentage);

            await _aggregateRepository.StoreAsync(book);
        }

        public async Task Handle(RemoveBookPromotionCommand command, CancellationToken cancellation)
        {
            var book = await _aggregateRepository.LoadAsync<Book>(command.Id);
            book.SetPromotedPrice(null);

            await _aggregateRepository.StoreAsync(book);
        }
    }
}
