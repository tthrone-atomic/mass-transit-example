using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface CheckOrderStatus
    {
        string OrderId { get; }
    }

    public interface OrderStatusResult
    {
        string OrderId { get; }
        DateTime Timestamp { get; }
        short StatusCode { get; }
        string StatusText { get; }
    }

    public class CheckOrderStatusConsumer :
    IConsumer<CheckOrderStatus>
    {
       // readonly IOrderRepository _orderRepository;

        public CheckOrderStatusConsumer()//IOrderRepository orderRepository)
        {
           // _orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<CheckOrderStatus> context)
        {
            // var order = await _orderRepository.Get(context.Message.OrderId);
            //if (order == null)
            //    throw new InvalidOperationException("Order not found");

            Console.WriteLine("OrderId: {0}", context.Message.OrderId);

            await context.RespondAsync<OrderStatusResult>(new
            {
                OrderId = "OrderId",
                Timestamp = DateTime.Now,
                StatusCode = 200,
                StatusText = "Success"
            });            
        }
    }


}
