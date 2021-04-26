using MassTransit;
using Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service2
{
    class Program
    {
        class UserEventConsumer : IConsumer<User>
        {
            public async Task Consume(ConsumeContext<User> context)
            {
                Console.WriteLine($"Value: {context.Message.Name} - {context.Message.Email}");
                await Task.CompletedTask;
            }
        }
        class AddStatmentConsumer : IConsumer<AddStatment>
        {
            public async Task Consume(ConsumeContext<AddStatment> context)
            {
                AdderResult sum  = new(context.Message.A + context.Message.B);
                await context.RespondAsync(sum);
            }
        }

        class MultiplyConsumer : IConsumer<MultiplyStatment>
        {
            public async Task Consume(ConsumeContext<MultiplyStatment> context)
            {
                MultiplyResult sum = new(context.Message.A * context.Message.B);
                await context.RespondAsync(sum);
            }
        }
        public static async Task Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.ReceiveEndpoint("user-listener", e => e.Consumer<UserEventConsumer>());
                cfg.ReceiveEndpoint("add-listener", e => e.Consumer<AddStatmentConsumer>());
                cfg.ReceiveEndpoint("multiply-listener", e => e.Consumer<MultiplyConsumer>());

            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

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
