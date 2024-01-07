using CQRS.Core.Queries.Abstract;
using Product.Messages.Events.Models;

namespace Product.Messages.Queries;

public class GetAllBooksQuery: IQuery<List<BookModel>>
{
    
}