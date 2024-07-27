using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        })
        .UseSerilog()
        .Build();

        await host.StartAsync();
    }

    static void BuildConfig(IConfigurationBuilder builder){
        builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true);
    }

    static void ConfigureServices(IServiceCollection services){

    }
}
