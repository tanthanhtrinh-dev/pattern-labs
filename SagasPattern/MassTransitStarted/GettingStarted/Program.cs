using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;


namespace GettingStarted
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<MessageConsumer>();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.ConfigureEndpoints(context);
                        });

                        //x.AddSagaStateMachine<OrderStateMachine, OrderState>()
                        //.MongoDbRepository(r => {
                        //    r.Connection = "mongodb://127.0.0.1";
                        //    r.DatabaseName = "orderdb";
                        //});
                    });

                    services.AddHostedService<Worker>();
                });
    }
}
