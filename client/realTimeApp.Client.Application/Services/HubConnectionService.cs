using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using realTimeApp.Client.Application;
using realTimeApp.Client.Application.Interfaces;
using realTimeApp.Client.Domain.Data;
using realTimeApp.Client.Domain.Data.Entities;
namespace realTimeApp.Client.Application.Services;

public class HubConnectionService : IHubConnectionService, IDisposable
{
    private HubConnection _hubConnection;
    private readonly ILogger<HubConnectionService> _logger;

    public HubConnectionService(ILogger<HubConnectionService> logger){
        _logger = logger;
    }

    public async Task<IDisposable> On<T>(string listeningName, Func<T, Task> handler)
    {
        return _hubConnection.On<T>(listeningName, handler);
    }

    public async Task<HubConnection> BuildConnection(string? hubPath = null)
    {
        string connPath = Shared.HostDomain + (hubPath is null ? Shared.HubPath : hubPath);

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(new Uri(connPath))
            .WithAutomaticReconnect()
            .Build();
                
        return _hubConnection;
    }

    public ref HubConnection GetHubConnection(){
        return ref _hubConnection;
    }

    public async void Dispose()
    {
        await _hubConnection.DisposeAsync();
    }

    public async Task<HubConnection> StartConnection()
    {
        await _hubConnection.StartAsync();
        return _hubConnection;
    }

    public async Task InvokeAsync(string methodName, string argument)
    {
        await _hubConnection.InvokeAsync(methodName, argument);
    }

    public async Task SendAsync(string methodName, string argument)
    {
        await _hubConnection.SendAsync(methodName, argument);
    }
}
