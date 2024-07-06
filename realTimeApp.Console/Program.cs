﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using realTimeApp.Application;
using realTimeApp.Domain;
using Serilog;
namespace realTimeApp;

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

        IHost host =  Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) => {
            services.AddSignalR();
            services.AddTransient<IHubService, HubService>();
            services.AddTransient<INotificationService, NotificationService>();
        })
        .UseSerilog()
        .Build();

        INotificationService notificationService = ActivatorUtilities.CreateInstance<NotificationService>(host.Services);

        Notification notification = new Notification{
            Header = "This is a header.",
            Text = "This is a text field."
        };

        notificationService.SendNotificationAsync(notification);

    }

    static void BuildConfig(IConfigurationBuilder builder){
        builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true);
    }
}