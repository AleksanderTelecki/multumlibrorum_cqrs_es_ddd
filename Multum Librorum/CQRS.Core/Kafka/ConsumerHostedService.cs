using CQRS.Core.Consumers;
using CQRS.Core.Kafka.Options;
using JasperFx.Core.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CQRS.Core.Kafka
{
    public class ConsumerHostedService : IHostedService
    {
        private readonly ILogger<ConsumerHostedService> _logger;
        private readonly IOptions<KafkaConsumerOptions> _kafkaConsumerOptions;
        private readonly IServiceProvider _serviceProvider;

        public ConsumerHostedService(ILogger<ConsumerHostedService> logger, IOptions<KafkaConsumerOptions> kafkaConsumerOptions, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _kafkaConsumerOptions = kafkaConsumerOptions;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event consumer service running.");

            if (_kafkaConsumerOptions.Value.Topics.Any())
                throw new ArgumentOutOfRangeException("Empty kafka topics list.");


            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();
                var topics = _kafkaConsumerOptions.Value.Topics;

                Task.Run(() => eventConsumer.Consume(topics), cancellationToken);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event consumer service stopped.");

            return Task.CompletedTask;
        }
    }
}
