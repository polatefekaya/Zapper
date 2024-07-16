using Microsoft.AspNetCore.SignalR;
using realTimeApp.Server.Application.Interfaces;

namespace realTimeApp.Server.Application.Services;

public class HubNotificationService : IHubNotificationService
{
    private readonly IHubContext<HubService> _hubContext;

    public HubNotificationService(IHubContext<HubService> hubContext){
        _hubContext = hubContext;
    }

    public async Task SendAddToGroup(string connectionId, string groupName)
    {
        await _hubContext.Clients.Group(groupName).SendAsync($"{connectionId} has been joined.");
    }

    public async Task SendRemoveFromGroup(string connectionId, string groupName)
    {
        await _hubContext.Clients.Group(groupName).SendAsync($"{connectionId} has been removed.");
    }
}
