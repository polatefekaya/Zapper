namespace realTimeApp.Server.Application.Interfaces;

public interface IHubNotificationService
{
    Task SendAddToGroup(string connectionId, string groupName);
    Task SendRemoveFromGroup(string connectionId, string groupName);
}
