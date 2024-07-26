using Microsoft.AspNetCore.SignalR;
using realTimeApp.Server.Domain.Data.Entities;
using realTimeApp.Server.Application.Interfaces;
using Microsoft.Extensions.Logging;
using realTimeApp.Server.Domain;

namespace realTimeApp.Server.Application.Services;

public class HubService : Hub, IHubService
{
    private readonly IHubNotificationService _hubNotificationService;
    private readonly IHubMessageService _hubMessageService;
    private readonly ILogger<HubService> _logger;
    public HubService(IHubNotificationService hubNotificationService, ILogger<HubService> logger, IHubMessageService hubMessageService){
        _hubNotificationService = hubNotificationService;
        _logger = logger;
        _hubMessageService = hubMessageService;
    }
    public override Task OnConnectedAsync()
    {
        Notification notification = new() {
            Header = "HeaderThis",
            Text = "Connected to the hub"
        };

        Task task = Clients.Caller.SendAsync("NotificationReceived", notification);

        return base.OnConnectedAsync();
    }

    public Task NotifyAll(Notification notification)
    {
        Task task = Clients.All.SendAsync("NotificationReceived", notification);
        return task;
    }

    public async Task AddToGroup(string groupName){
        _logger.LogInformation("Adding {id} to {gname} is started", Context.ConnectionId, groupName);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName); 
        await _hubNotificationService.SendAddToGroup(Context.ConnectionId, groupName);
    }

    public async Task RemoveFromGroup(string groupName){
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        await _hubNotificationService.SendRemoveFromGroup(Context.ConnectionId, groupName);
    }

    public async Task SendMessageToGroup(string groupName, MessageEntity message){
        message.Sender = Context.ConnectionId;
        message.sentTime = DateTime.UtcNow;
        await _hubMessageService.SendMessageToGroup(groupName, message);
    }

    public async Task SendSecureMessageToGroup(string groupName, SecureMessageEntity message){
        message.Sender = Context.ConnectionId;
        message.sentTime = DateTime.UtcNow;
        await _hubMessageService.SendSecureMessageToGroup(groupName, message);
    }
}
