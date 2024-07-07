using realTimeApp.Server.Domain.Data.Entities;

namespace realTimeApp.Server.Application.Interfaces;

public interface INotificationService
{
    Task SendNotificationAsync(Notification notification);
}
