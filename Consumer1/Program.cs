using Common;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(Environment.GetEnvironmentVariable("RabbitMQ__Host")), h =>
                {
                    h.Username(Environment.GetEnvironmentVariable("RabbitMQ__User"));
                    h.Password(Environment.GetEnvironmentVariable("RabbitMQ__Pass"));
                });

                cfg.ReceiveEndpoint("event-listener", e =>
                {
                    e.Consumer<EventConsumer>();
                });

                cfg.ReceiveEndpoint("CheckOrderStatus", e =>
                {
                    e.Consumer<CheckOrderStatusConsumer>();
                });
            });

            var source = new CancellationTokenSource();
            await busControl.StartAsync(source.Token);
            try
            {
                Console.WriteLine("Press enter to exit");

                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
