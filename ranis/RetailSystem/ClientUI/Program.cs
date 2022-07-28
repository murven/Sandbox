using Messages;
using NServiceBus;
Console.Title = "ClientUI";
var endpointConfiguration = new EndpointConfiguration("ClientUI");
// This will create the queues and exchanges in RabbitMQ:
endpointConfiguration.EnableInstallers();
endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
transport.UseConventionalRoutingTopology(QueueType.Quorum);
var connectionString =
@"host=localhost;
username=guest;
password=guest";
transport.ConnectionString(connectionString: connectionString);
var routing = transport.Routing();
routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");
endpointConfiguration.SendOnly();
var endpoint = await Endpoint.Start(endpointConfiguration);
await RunLoop(endpoint);
await endpoint.Stop();

static async Task RunLoop(IMessageSession endpoint)
{
    var shouldExit = false;
    do
    {
        Console.WriteLine("Press 'P' to send order, 'Escape' to exit.");
        var keyInformation = Console.ReadKey(intercept: true);
        switch (keyInformation.Key)
        {
            case ConsoleKey.Escape:
                shouldExit = true;
                break;
            case ConsoleKey.P:
                await endpoint.Send(CreatePlaceOrderCommand());
                break;
            default:
                break;
        }
    } while (!shouldExit);
}

static PlaceOrder CreatePlaceOrderCommand() => new PlaceOrder(orderId: Guid.NewGuid());