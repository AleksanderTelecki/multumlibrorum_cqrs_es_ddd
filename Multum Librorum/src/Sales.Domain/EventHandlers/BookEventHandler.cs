using CQRS.Core.Events;
using CQRS.Core.Events.Abstract;
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
        public Task Handle(BookAddedEvent @event, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handle BookAdded Message");

            return Task.CompletedTask;
        }
    }
}
