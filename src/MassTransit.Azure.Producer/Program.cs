using System;
using System.Threading.Tasks;
using MassTransit.Azure.ServiceBus.Core;
using MassTransit.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.Azure.Producer
{
    class Program
    {
        private static string connectionString = "";

        static async Task Main(string[] args)
        {
            var bus = SetupBus();

            await RunApplication(bus);
        }

        // 初始化 MassTransit 的 IBusControl
        private static IBusControl SetupBus()
        {
            var services = new ServiceCollection();

            // 配置 MassTransit
            services.AddMassTransit(x =>
            {
                // 使用 ASB 作为 transport provider
                x.AddBus(provider => Bus.Factory.CreateUsingAzureServiceBus(serviceBus =>
                {
                    serviceBus.Host(connectionString);
                }));
            });
            // 从 DI container 中获得实例化
            return services.BuildServiceProvider().GetService<IBusControl>();
        }

        private static async Task RunApplication(IBusControl bus)
        {
            do
            {
                Console.WriteLine("Sending message....");

                await bus.Publish(new DoThing { Id = Guid.NewGuid().ToString() });

                Console.WriteLine("Press any key to send message...., Esc to exit");

                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }

            } while (true);
        }
    }
}
