using CQRS.Core.Queries.Abstract;
using Sales.Messages.Models;

namespace Sales.Messages.Queries;

public class GetOrdersQuery: IQuery<List<OrderModel>>
{
    
}