using Microsoft.AspNetCore.SignalR.Client;
using realTimeApp.Client.Application;
using realTimeApp.Client.Application.Interfaces;
using realTimeApp.Client.Domain.Data;
using realTimeApp.Client.Domain.Data.Entities;
namespace realTimeApp.Client.Application.Services;

public class HubConnectionService : IHubConnectionService, IDisposable
{
    private HubConnection _hubConnection;

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

    public HubConnection GetHubConnection(){
        return _hubConnection;
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
}
