using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales;

internal class PlaceOrderHandler : IHandleMessages<PlaceOrder>
{
    public Task Handle(PlaceOrder message, IMessageHandlerContext context)
    {
        _logger.InfoFormat(
            format: "Received {commandName} command. Id: ({commandId})",
            nameof(PlaceOrder),
            message.OrderId
            );
        return Task.CompletedTask;
    }

    static ILog _logger = LogManager.GetLogger<PlaceOrderHandler>();
}
