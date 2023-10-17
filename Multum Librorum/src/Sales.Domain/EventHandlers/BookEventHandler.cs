using CQRS.Core.Events;
using Product.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.EventHandlers
{
    public class BookEventHandler :
        IEventHandler<BookAddedEvent>
    {
        public Task Handle(BookAddedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handle BookAdded Message");

            return Task.CompletedTask;
        }
    }
}
