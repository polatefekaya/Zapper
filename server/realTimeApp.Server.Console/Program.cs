﻿using realTimeApp.Server.Application;
using realTimeApp.Server.Domain;
using Serilog;
namespace realTimeApp.Server.Console;

class Program
{
    static void Main(string[] args)
    {
        ConfigurationBuilder builder = new ConfigurationBuilder();
        BuildConfig(builder);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        Log.Logger.Information("Console App is Started");
        
        IHost host =  Host.CreateDefaultBuilder(args)
        .ConfigureWebHost(webHost => {
            webHost.UseKestrel(kestrel => {
                kestrel.ListenAnyIP(5050);
            }).ConfigureServices((context, services) => {
                services.AddSignalR();
                services.AddTransient<IHubService, HubService>();
                services.AddTransient<INotificationService, NotificationService>();
            })
            .Configure(app => {
                app.UseRouting();
                app.UseEndpoints(endpoints => {
                        endpoints.MapHub<HubService>("/hub/notifications");
                    });
                app.Run(async context =>{
                    //await context.Response.WriteAsync("Working");

                    INotificationService notificationService = ActivatorUtilities.CreateInstance<NotificationService>(app.ApplicationServices);

                    Notification notification = new Notification{
                        Header = "This is a header.",
                        Text = "This is a text field."
                    };

                    await notificationService.SendNotificationAsync(notification);

                    
                }
                );
            });
        })
        .UseSerilog()
        .Build();

        host.Run();
    }

    static void BuildConfig(IConfigurationBuilder builder){
        builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true);
    }
}