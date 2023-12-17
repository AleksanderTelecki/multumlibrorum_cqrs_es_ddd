using CQRS.Core.Events.Abstract;
using CQRS.Core.Events;
using CQRS.Core.Queries.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public QueryDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public Task<TResult> Dispatch<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            var queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            try 
            {
                dynamic handler = _serviceProvider.GetRequiredService(queryHandlerType);
                return handler.Handle((dynamic)query, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine($"Handler not registered for the query of type: {queryHandlerType.FullName}");
                return (Task<TResult>)Task.CompletedTask;
            }
        }
    }
}
