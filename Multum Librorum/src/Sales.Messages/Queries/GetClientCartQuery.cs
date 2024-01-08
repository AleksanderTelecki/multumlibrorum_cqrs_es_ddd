using CQRS.Core.Queries.Abstract;
using Sales.Messages.Models;

namespace Sales.Messages.Queries;

public class GetClientCartQuery: IQuery<List<CartItemModel>>
{
    public Guid ClientId { get; set; }
}