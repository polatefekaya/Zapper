using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using realTimeApp.Server.Domain.Data.Entities;
using realTimeApp.Server.Application.Interfaces;

namespace realTimeApp.Server.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<HubService> _hubContext;
    private readonly ILogger<NotificationService> _logger;
    public NotificationService(IHubContext<HubService> hubContext, ILogger<NotificationService> logger) {
        _hubContext = hubContext;
        _logger = logger;
    }

    public Task SendNotificationAsync(Notification notification)
    {
        _logger.LogInformation("Notification Sending Is Started");
        Task notificationTask = _hubContext.Clients.All.SendAsync("NotificationReceived", notification);
        _logger.LogInformation("Notification Sent");
        _logger.LogInformation("Header = {header}, Text = {text}", notification.Header, notification.Text);
        return notificationTask;
    }
}
