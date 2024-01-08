using CQRS.Core.Queries.Abstract;
using Product.Messages.Events.Models;

namespace Product.Messages.Queries;

public class GetBookModelQuery: IQuery<BookModel>
{
    public Guid ProductId { get; set; }
}