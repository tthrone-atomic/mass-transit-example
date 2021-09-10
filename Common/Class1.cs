using MassTransit;
using System;
using System.Threading.Tasks;

namespace Common
{
    public interface ValueEntered
    {
        string Value { get; }
    }

    public class EventConsumer : IConsumer<ValueEntered>
    {
        public async Task Consume(ConsumeContext<ValueEntered> context)
        {
            Console.WriteLine("Value: {0}", context.Message.Value);
        }
    }
}
