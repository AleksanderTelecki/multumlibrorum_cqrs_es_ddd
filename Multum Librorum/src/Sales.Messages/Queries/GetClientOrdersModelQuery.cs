using CQRS.Core.Queries.Abstract;
using Sales.Messages.Models;

namespace Sales.Messages.Queries;

public class GetClientOrdersModelQuery: IQuery<List<OrderModel>>
{
    public Guid ClientId { get; set; }
}