using CQRS.Core.Events;
using Sales.Messages.Enums;

namespace Sales.Messages.Events;

public class OrderStateUpdatedEvent: Event
{
    public OrderState NewOrderState { get; set; }
}