using Microsoft.AspNetCore.SignalR;
using realTimeApp.Server.Application.Interfaces;
using realTimeApp.Server.Domain.Data.Entities;

namespace realTimeApp.Server.Application.Services;

public class HubNotificationService : IHubNotificationService
{
    private readonly IHubContext<HubService> _hubContext;

    public HubNotificationService(IHubContext<HubService> hubContext){
        _hubContext = hubContext;
    }

    public async Task SendAddToGroup(string connectionId, string groupName)
    {
        Notification notification = new Notification{
            Header = $"{connectionId} has been joined",
            Text = "Say hello to new friend"
        };
        await _hubContext.Clients.Group(groupName).SendAsync("NotificationReceived",notification);
    }

    public async Task SendRemoveFromGroup(string connectionId, string groupName)
    {
        Notification notification = new Notification{
            Header = $"{connectionId} has been removed",
            Text = "Say goodbye to old friend"
        };
        await _hubContext.Clients.Group(groupName).SendAsync("NotificationReceived", notification);
    }
}
