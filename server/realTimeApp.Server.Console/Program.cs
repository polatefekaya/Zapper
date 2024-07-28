using realTimeApp.Server.Application.Services;
using realTimeApp.Server.Application.Interfaces;
using realTimeApp.Server.Domain.Data.Entities;
using Serilog;
using realTimeApp.Server.Application;
using MassTransit;
using MassTransit.Transports.Fabric;
namespace realTimeApp.Server.Console;

class Program
{
    static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        Log.Logger.Information("Console App is Started");

        builder.WebHost.UseKestrel(kestrel => {
            kestrel.ListenAnyIP(5050);
        });

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(Log.Logger);

        builder.Services.AddSignalR();
        builder.Services.AddMassTransit(busConfigurator => {
            busConfigurator.SetDefaultEndpointNameFormatter();
  
            busConfigurator.UsingRabbitMq((context, configurator) => {
                configurator.Host(
                host: builder.Configuration["MessageBroker:Host"]!,
                virtualHost: builder.Configuration["MessageBroker:Virtual"]!, 
                configure: h => {
                    h.Username(builder.Configuration["MessageBroker:UserName"]!);
                    h.Password(builder.Configuration["MessageBroker:Password"]!);
                });
                configurator.Publish<SecureMessageEntity>(cfg => {
                    cfg.ExchangeType = "fanout";
                });
                
                configurator.ConfigureEndpoints(context);
            });
        });

        builder.Services.AddTransient<IHubService, HubService>();
        builder.Services.AddTransient<INotificationService, NotificationService>();
        builder.Services.AddTransient<ISignalSenderService, SignalSenderService>();
        builder.Services.AddTransient<IHubNotificationService, HubNotificationService>();
        builder.Services.AddTransient<IHubMessageService, HubMessageService>();

        builder.Services.AddControllers();

        System.Console.WriteLine(builder.Configuration["RandomInfo"]);

        var app = builder.Build();

        app.UseRouting();
        app.MapHub<HubService>("/hub/notifications");
        app.MapControllers();


        app.Run();
    }
}
