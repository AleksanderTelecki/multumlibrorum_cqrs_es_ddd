using CQRS.Core.Events.Abstract;
using KafkaFlow;

namespace Kafka.Core.Middleware
{
    public class MediatorMiddleware : IMessageMiddleware
    {
        private readonly IEventDispatcher _eventDispatcher;

        public MediatorMiddleware(IEventDispatcher eventDispatcher) 
        {
            _eventDispatcher = eventDispatcher;
        }

        public async Task Invoke(IMessageContext context, MiddlewareDelegate next)
        {
            await _eventDispatcher.Dispatch(context.Message.Value, context.ConsumerContext.WorkerStopped);
            await next(context);
        }
    }
}
