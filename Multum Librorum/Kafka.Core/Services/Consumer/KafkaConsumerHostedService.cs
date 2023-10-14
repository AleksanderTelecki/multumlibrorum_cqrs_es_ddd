using Kafka.Core.Options;
using KafkaFlow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Kafka.Core.Services.Consumer
{
    public class KafkaConsumerHostedService : IHostedService
    {
        private readonly ILogger<KafkaConsumerHostedService> _logger;
        private readonly IOptions<KafkaConsumerOptions> _kafkaConsumerOptions;
        private readonly IServiceProvider _serviceProvider;
        private IKafkaBus _kafkaBus;

        public KafkaConsumerHostedService(ILogger<KafkaConsumerHostedService> logger, IOptions<KafkaConsumerOptions> kafkaConsumerOptions, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _kafkaConsumerOptions = kafkaConsumerOptions;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event consumer service running.");

            if (!_kafkaConsumerOptions.Value.Topics.Any())
                throw new ArgumentOutOfRangeException("Empty kafka topics list.");

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                _kafkaBus = scope.ServiceProvider.CreateKafkaBus();
                _kafkaBus.StartAsync();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _kafkaBus.StopAsync();
            _logger.LogInformation("Event consumer service stopped.");

            return Task.CompletedTask;
        }
    }
}
