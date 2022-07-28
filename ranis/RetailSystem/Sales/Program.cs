using Microsoft.Extensions.Hosting;
using NServiceBus;
Console.Title = "Sales";
await Host
    .CreateDefaultBuilder(args)
    .UseNServiceBus(ConfigureEndpoint)
    .RunConsoleAsync();

EndpointConfiguration ConfigureEndpoint(HostBuilderContext context)
{
    var endpointConfiguration = new EndpointConfiguration("Sales");
    // This will create the queues and exchanges in RabbitMQ:
    endpointConfiguration.EnableInstallers();
    endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
    var transport = endpointConfiguration
        .UseTransport<RabbitMQTransport>();
    transport.UseConventionalRoutingTopology(QueueType.Quorum);
    var connectionString =
@"host=localhost;
username=guest;
password=guest";
    transport.ConnectionString(connectionString: connectionString);
    return endpointConfiguration;
}