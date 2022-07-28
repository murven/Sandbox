using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages;

public class PlaceOrder : ICommand
{
    public Guid OrderId { get; }
    public PlaceOrder(Guid orderId)
    {
        OrderId = orderId;
    }
}
