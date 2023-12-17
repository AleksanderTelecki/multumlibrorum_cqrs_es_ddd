using CQRS.Core.Events;

namespace Product.Messages.Events;

public class BookQuantityDecreasedEvent: Event
{
    public int BookDecreasedQuantity { get; set; }
}