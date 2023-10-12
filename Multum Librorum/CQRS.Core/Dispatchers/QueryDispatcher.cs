using CQRS.Core.Infrastructure;
using CQRS.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly Dictionary<Type, Delegate> _handlers = new();

        public void RegisterHandler<TQuery, QResult>(Func<TQuery, Task<QResult>> handler) where TQuery : Query<QResult>
        {
            if (_handlers.ContainsKey(typeof(TQuery)))
                throw new IndexOutOfRangeException("You cannot register same query twice!");

            _handlers.Add(typeof(TQuery), handler);
        }

        public async Task<R> SendAsync<R>(Query<R> query)
        {
            if (_handlers.TryGetValue(query.GetType(), out var handler))
            {
                var result = handler.DynamicInvoke(query);
                return await (Task<R>)result;
            }

            throw new ArgumentNullException(nameof(handler), "No query handler was registered!");
        }
    }
}
