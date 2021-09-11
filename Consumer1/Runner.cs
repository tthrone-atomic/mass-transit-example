using Common;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer1
{
    public class Runner : IHostedService
    {
        private readonly ILogger<Runner> _logger;
        private IBusControl _busControl;

        public Runner(ILogger<Runner> logger)
        {
            _logger = logger;

            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
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

        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _busControl.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
           await _busControl.StopAsync();
        }
    }
}
