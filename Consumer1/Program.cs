using Common;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder()
                 .ConfigureServices((hostContext, services) =>
                 {
                     services.AddHostedService<Runner>();
                 });

            await hostBuilder.RunConsoleAsync();
        }
    }
}
