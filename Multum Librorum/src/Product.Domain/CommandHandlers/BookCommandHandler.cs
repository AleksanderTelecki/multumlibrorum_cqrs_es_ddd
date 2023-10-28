using CQRS.Core.Commands.Abstract;
using Marte.EventSourcing.Core.Abstract;
using Product.Domain.Aggregates;
using Product.Domain.Repository;
using Product.Messages.Commands;

namespace Product.Domain.CommandHandlers
{
    public class BookCommandHandler :
        ICommandHandler<AddBookCommand>
    {
        private readonly IAggregateReporitory _aggregateReporitory;

        public BookCommandHandler(IAggregateReporitory aggregateReporitory)
        {
            _aggregateReporitory = aggregateReporitory;
        }

        public async Task Handle(AddBookCommand command, CancellationToken cancellationToken)
        {
            var book = new Book(command.Title, command.Author, command.Price, command.PageCount);
            await _aggregateReporitory.StoreAsync(book);
        }
    }
}
