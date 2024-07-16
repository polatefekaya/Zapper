using realTimeApp.Server.Domain.Data.Entities;

namespace realTimeApp.Server.Application.Interfaces;

public interface IHubService
{
    Task NotifyAll(Notification notification);
    Task AddToGroup(string groupName);
    Task RemoveFromGroup(string groupName);
}
