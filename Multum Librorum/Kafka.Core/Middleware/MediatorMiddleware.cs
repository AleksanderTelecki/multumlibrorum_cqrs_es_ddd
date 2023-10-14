using KafkaFlow;
using MediatR;

namespace Kafka.Core.Middleware
{
    public class MediatorMiddleware : IMessageMiddleware
    {
        private readonly IMediator _mediator;

        public MediatorMiddleware(IMediator mediator) 
        {
            _mediator = mediator;
        }

        public async Task Invoke(IMessageContext context, MiddlewareDelegate next)
        {
            await _mediator.Publish(context.Message.Value, context.ConsumerContext.WorkerStopped);

            await next(context);
        }
    }
}
