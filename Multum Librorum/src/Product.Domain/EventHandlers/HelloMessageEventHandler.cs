using KafkaFlow;
using KafkaFlow.TypedHandler;
using MediatR;
using Product.Messages.Events;

namespace Product.Domain.EventHandlers
{
    public class HelloMessageEventHandler : INotificationHandler<HelloMessage>
    {
        public Task Handle(HelloMessage notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Hello World!");

            return Task.CompletedTask;
        }
    }
}
