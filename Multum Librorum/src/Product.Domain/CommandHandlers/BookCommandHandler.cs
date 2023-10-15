using CQRS.Core.Commands;
using Marte.EventSourcing.Core.Abstract;
using Product.Domain.Aggregates;
using Product.Messages.Commands;

namespace Product.Domain.CommandHandlers
{
    public class BookCommandHandler :
        ICommandHandler<AddBookCommand>
    {
        private readonly ProductDomainDataContext _productDomainDataContext;
        private readonly IAggregateReporitory _aggregateReporitory;

        public BookCommandHandler(ProductDomainDataContext productDomainDataContext, IAggregateReporitory aggregateReporitory)
        {
            _productDomainDataContext = productDomainDataContext;
            _aggregateReporitory = aggregateReporitory;
        }

        public async Task Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book(request.Title, request.Author, request.Price, request.PageCount);
            await _aggregateReporitory.StoreAsync(book);
        }
    }
}
