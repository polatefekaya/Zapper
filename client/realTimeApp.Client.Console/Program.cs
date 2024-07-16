using realTimeApp.Client.Domain.Data.Entities;
using Microsoft.AspNetCore.SignalR.Client;
using realTimeApp.Client.Application.Interfaces;
using realTimeApp.Client.Application.Services;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace realTimeApp.Client.Console;

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
        })
        .UseSerilog()
        .Build();

        await host.StartAsync();

        IHubConnectionService hubConnectionService = host.Services.GetRequiredService<IHubConnectionService>();

        await hubConnectionService.BuildConnection();
        await hubConnectionService.StartConnection();

        //_hubConnection.InvokeCoreAsync("NotifyAll", new[]{notification}).Wait();
        await hubConnectionService.On<Notification>("NotificationReceived", OnNotificationReceivedAsync);

        while(true){
            string? line = System.Console.ReadLine();
            if(line is not null && line.Equals("exit")){
                break;
            }
        }
    }

    private static async Task OnNotificationReceivedAsync(Notification notification)
    {
        // Do something meaningful with the notification.
        System.Console.WriteLine(notification.Header + "\n" + notification.Text);
        await Task.CompletedTask;
    }

    static void BuildConfig(IConfigurationBuilder builder){
        builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true);
    }

    static void ConfigureServices(IServiceCollection services){
        services.AddTransient<IHubConnectionService, HubConnectionService>();
    }
}
