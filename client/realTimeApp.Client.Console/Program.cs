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
        IMessageService messageService = host.Services.GetRequiredService<IMessageService>();
        ICommandManagerService commandManagerService = host.Services.GetRequiredService<ICommandManagerService>();

        ISessionManagerService sessionManager = host.Services.GetRequiredService<ISessionManagerService>();

        await hubConnectionService.BuildConnection();
        await hubConnectionService.StartConnection();

        //_hubConnection.InvokeCoreAsync("NotifyAll", new[]{notification}).Wait();
        await hubConnectionService.On<Notification>("NotificationReceived", OnNotificationReceivedAsync);
        await hubConnectionService.On<MessageEntity>("MessageReceived", OnMessageReceivedAsync);

        //await hubConnectionService.InvokeAsync("AddToGroup", "group1");

        again:
        System.Console.Clear();

        if(sessionManager.GetCurrentSession() is not null){
            System.Console.WriteLine($"Session: {sessionManager.GetCurrentSession()}");
        }

        if(sessionManager.GetCurrentSessionType() is not null){
            System.Console.WriteLine($"Session Type: {sessionManager.GetCurrentSessionType()}");
        }
        System.Console.WriteLine("If you not joined to a group, you can not message anybody");
        System.Console.WriteLine("message or group?");
        string? line = System.Console.ReadLine();
        if(line is not null){
            int exitType = 0;
            exitType = await commandManagerService.Start(line);
            //if(exitType == 1) break;
            if(exitType == 2) goto again;
        }
    }

    private static async Task OnNotificationReceivedAsync(Notification notification)
    {
        // Do something meaningful with the notification.
        System.Console.WriteLine(notification.Header + "\n" + notification.Text);
        await Task.CompletedTask;
    }

    private static async Task OnMessageReceivedAsync(MessageEntity message){
        System.Console.WriteLine(message.Sender + ": " + message.Body + "\n" + message.sentTime);
        await Task.CompletedTask;
    }

    static void BuildConfig(IConfigurationBuilder builder){
        builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true);
    }

    static void ConfigureServices(IServiceCollection services){
        services.AddSingleton<IHubConnectionService, HubConnectionService>();
        services.AddTransient<IGroupConnectionService, GroupConnectionService>();
        services.AddTransient<IMessageService, MessageService>();
        services.AddSingleton<ISessionManagerService, SessionManagerService>();
        services.AddSingleton<ICommandManagerService, CommandManagerService>();
        services.AddTransient<IMessagingCommandsService, MessagingCommandsService>();
        services.AddTransient<IGroupCommandsService, GroupCommandsService>();
    }
}
