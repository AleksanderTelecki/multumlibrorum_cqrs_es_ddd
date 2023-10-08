using CQRS.Core.Events;
using CQRS.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Dispatchers
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly Dictionary<Type, Func<Event, Task>> _handlers = new();

        public void RegisterHandler<T>(Func<T, Task> handler) where T : Event
        {
            if (_handlers.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException("You cannot register the same event handler twice!");
            }

            _handlers.Add(typeof(T), x => handler((T)x));
        }

        public async Task On(Event @event)
        {
            if (_handlers.TryGetValue(@event.GetType(), out var handler))
            {
                await handler(@event);
            }
            else
            {
                throw new ArgumentNullException("No event handler was register");
            }
        }
    }
}
