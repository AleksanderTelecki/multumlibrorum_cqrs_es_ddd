using Kafka.Core.Options;
using Kafka.Core.Services.Consumer;
using Kafka.Core.Extensions;
using KafkaFlow;
using KafkaFlow.Producers;
using KafkaFlow.Serializer;
using Product.Domain.EventHandlers;
using Product.Messages.Events;
using KafkaFlow.Retry;
using Kafka.Core.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configurations = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// Variables

var kafkaProduceConf = builder.Configuration.GetSection(nameof(KafkaProducerOptions)).Get<KafkaProducerOptions>();
var kafkaConsumerConf = builder.Configuration.GetSection(nameof(KafkaConsumerOptions)).Get<KafkaConsumerOptions>();
var kafkaBootstrapServer = builder.Configuration.GetConnectionString("KafkaBootstrapServer")!;

// Configurations
builder.Services.Configure<KafkaConsumerOptions>(builder.Configuration.GetSection(nameof(KafkaConsumerOptions)));
builder.Services.Configure<KafkaProducerOptions>(builder.Configuration.GetSection(nameof(KafkaProducerOptions)));

// Services
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblyContaining<DummyHandler>();
});

// Kafka 
builder.Services.AddKafka(
    kafka => kafka
        .UseConsoleLog()
        // Producer
        .AddCluster(
            cluster => cluster
                .WithBrokers(new[] { kafkaBootstrapServer })
                .CreateTopicIfNotExists(kafkaProduceConf.ProducerTopic, 1, 1)
                .AddProducer(
                    kafkaProduceConf.ProducerName,
                    producer => producer
                        .DefaultTopic(kafkaProduceConf.ProducerTopic)
                        .AddMiddlewares(m =>
                            m.AddSerializer<JsonCoreSerializer>()
                            )
                )
        )
        // Consumer
        .AddCluster(cluster => cluster
        .WithBrokers(new[] { kafkaBootstrapServer })
        .AddTopicAutoGeneration(kafkaConsumerConf.Topics)
        .AddConsumer(consumer => consumer
            .Topics(kafkaConsumerConf.Topics)
            .WithGroupId(kafkaConsumerConf.TopicGroup)
            .WithBufferSize(100)
            .WithWorkersCount(10)
            .WithAutoOffsetReset(KafkaFlow.AutoOffsetReset.Earliest)
            .AddMiddlewares(middlewares => middlewares
                .RetrySimple(
                    (config) => config
                        .HandleAnyException()
                        .TryTimes(3)
                        .WithTimeBetweenTriesPlan((retryCount) => TimeSpan.FromMilliseconds(Math.Pow(2, retryCount) * 1000))
                )
                .AddSerializer<JsonCoreSerializer>()
                .Add<MediatorMiddleware>(MiddlewareLifetime.Transient)
            )
        )
    )
);


builder.Services.AddHostedService<KafkaConsumerHostedService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


var serviceProvider = builder.Services.BuildServiceProvider();

var producer = serviceProvider
    .GetRequiredService<IProducerAccessor>()
    .GetProducer(kafkaProduceConf.ProducerName);

await producer.ProduceAsync(
                   kafkaProduceConf.ProducerTopic,
                   Guid.NewGuid().ToString(),
                   new HelloMessage { Name = "Hello!" });



app.Run();


