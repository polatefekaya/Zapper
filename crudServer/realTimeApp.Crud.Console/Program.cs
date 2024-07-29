using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using realTimeApp.Crud.Application.Interfaces.Consumers;
using realTimeApp.Crud.Application.Services.Consumers;
using Serilog;

namespace realTimeApp.Crud.Console;

class Program
{
    static async Task Main(string[] args)
    {
        ConfigurationBuilder builder = new ConfigurationBuilder();
        BuildConfig(builder);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((context, services) => {
            ConfigureServices(services);
            
            services.AddMassTransit(busConfigurator => {
            busConfigurator.SetDefaultEndpointNameFormatter();

            busConfigurator.AddConsumer<SecureMessageConsumerService>();

            busConfigurator.UsingRabbitMq((ctx, configurator) => {
                configurator.Host(
                host: context.Configuration["MessageBroker:Host"]!,
                virtualHost: context.Configuration["MessageBroker:Virtual"]!, 
                configure: h => {
                    h.Username(context.Configuration["MessageBroker:UserName"]!);
                    h.Password(context.Configuration["MessageBroker:Password"]!);
                    
                });


                configurator.ConfigureEndpoints(ctx);
            });
        });
        })
        .UseSerilog()
        .Build();

        await host.StartAsync();

        System.Console.ReadLine();
    }

    static void BuildConfig(IConfigurationBuilder builder){
        builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true);
    }

    static void ConfigureServices(IServiceCollection services){
        services.AddSingleton<ISecureMessageConsumerService, SecureMessageConsumerService>();
    }
}
