using CQRS.Core.Queries.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Events.Abstract
{
    public interface IEventHandler<TEvent> where TEvent : Event
    {
        Task Handle(TEvent @event, CancellationToken cancellation);
    }

}
