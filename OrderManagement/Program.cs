using Newtonsoft.Json;
using OrderManagement.Definition;

var builder = WebApplication.CreateBuilder(args);

var endpointConfiguration = new EndpointConfiguration("order");


// Configure RabbitMQ transport
var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
    .UseConventionalRoutingTopology(QueueType.Quorum);
transport.ConnectionString("host=localhost"); // Adjust the connection string as needed

var routing = transport.Routing();
routing.RouteToEndpoint(typeof(OrderCreatedEvent), "order");
routing.RouteToEndpoint(typeof(PaymentHandledEvent), "order");
endpointConfiguration.EnableInstallers();

// Persistence configuration (use LearningPersistence for development)
var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();

// Concurrency and recoverability settings
endpointConfiguration.LimitMessageProcessingConcurrencyTo(5);

var recoverability = endpointConfiguration.Recoverability();
recoverability.Immediate(immediate => immediate.NumberOfRetries(3));
recoverability.Delayed(delayed => delayed.NumberOfRetries(3).TimeIncrease(TimeSpan.FromSeconds(5)));

// Specify JSON serializer using Newtonsoft.Json
var serialization = endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
var settings = new JsonSerializerSettings
{
    TypeNameHandling = TypeNameHandling.Auto,
    Formatting = Formatting.None
};
serialization.Settings(settings);

// Register NServiceBus endpoint
builder.Host.UseNServiceBus(_ => endpointConfiguration);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();