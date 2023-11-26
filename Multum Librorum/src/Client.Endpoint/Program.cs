using Kafka.Core.Options;
using Kafka.Core.Services.Consumer;
using Kafka.Core.Extensions;
using KafkaFlow;
using KafkaFlow.Serializer;
using KafkaFlow.Retry;
using Kafka.Core.Middleware;
using Marten;
using Weasel.Core;
using Microsoft.EntityFrameworkCore;
using Marte.EventSourcing.Core.Abstract;
using Marten.EventSourcing.Core;
using Kafka.Core.Abstract;
using Kafka.Core.Services.Producer;
using CQRS.Core.Extensions;
using Client.Domain;
using Client.Domain.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Variables
var kafkaProduceConf = builder.Configuration.GetSection(nameof(KafkaProducerOptions)).Get<KafkaProducerOptions>();
var kafkaConsumerConf = builder.Configuration.GetSection(nameof(KafkaConsumerOptions)).Get<KafkaConsumerOptions>();
var kafkaBootstrapServer = builder.Configuration.GetConnectionString("KafkaBootstrapServer")!;

// Configurations
builder.Services.AddDbContext<ClientDomainDataContext>(o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.Configure<KafkaConsumerOptions>(builder.Configuration.GetSection(nameof(KafkaConsumerOptions)));
builder.Services.Configure<KafkaProducerOptions>(builder.Configuration.GetSection(nameof(KafkaProducerOptions)));

// Services
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IAggregateReporitory, AggregateRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();


// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddMarten(options =>
{
    // Establish the connection string to your Marten database
    options.Connection(builder.Configuration.GetConnectionString("Marten")!);

    // If we're running in development mode, let Marten just take care
    // of all necessary schema building and patching behind the scenes
    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
});

builder.Services.ConfigureCQRS<Dummy>();

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

app.Run();


