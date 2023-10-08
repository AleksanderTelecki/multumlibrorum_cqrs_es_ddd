using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Infrastructure
{
    public interface IEventDispatcher
    {
        void RegisterHandler<T>(Func<T, Task> handler) where T : Event;
        Task On(Event @event);
    }
}
