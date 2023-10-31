using CQRS.Core.Events.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;

namespace CQRS.Core.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public Task Dispatch(object @event, CancellationToken cancellation = default)
        {
            var eventHandlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());

            try
            {
                dynamic handler = _serviceProvider.GetRequiredService(eventHandlerType);
                return handler.Handle((dynamic)@event, cancellation);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine($"Handler not registered for the event of type: {eventHandlerType.FullName}");
                return Task.CompletedTask;
            }
        }
    }
}
