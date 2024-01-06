using CQRS.Core.Events;
using Sales.Messages.Models;

namespace Sales.Messages.Events;

public class OrderProductsAddedEvent: Event
{
    public ICollection<OrderItem> OrderItems { get; set; }
}