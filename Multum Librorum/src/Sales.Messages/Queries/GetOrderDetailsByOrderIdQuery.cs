using CQRS.Core.Queries.Abstract;
using Sales.Messages.Models;

namespace Sales.Messages.Queries;

public class GetOrderDetailsByOrderIdQuery: IQuery<OrderModel>
{
    public Guid OrderId { get; set; }
}