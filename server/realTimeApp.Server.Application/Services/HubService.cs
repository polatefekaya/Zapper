using Microsoft.AspNetCore.SignalR;
using realTimeApp.Server.Domain.Data.Entities;
using realTimeApp.Server.Application.Interfaces;

namespace realTimeApp.Server.Application.Services;

public class HubService : Hub, IHubService
{
    private readonly IHubNotificationService _hubNotificationService;
    public HubService(IHubNotificationService hubNotificationService){
        _hubNotificationService = hubNotificationService;
    }
    public override Task OnConnectedAsync()
    {
        Notification notification = new() {
            Header = "HeaderThis",
            Text = "Connected to the hub"
        };

        Task task = Clients.All.SendAsync("NotificationReceived", notification);

        return base.OnConnectedAsync();
    }

    public Task NotifyAll(Notification notification)
    {
        Task task = Clients.All.SendAsync("NotificationReceived", notification);
        return task;
    }

    public async Task AddToGroup(string groupName){
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName); 
        await _hubNotificationService.SendAddToGroup(Context.ConnectionId, groupName);
    }

    public async Task RemoveFromGroup(string groupName){
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        await _hubNotificationService.SendRemoveFromGroup(Context.ConnectionId, groupName);
    }
}
