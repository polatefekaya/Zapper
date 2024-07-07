using realTimeApp.Server.Domain;

namespace realTimeApp.Server.Application;

public interface INotificationService
{
    Task SendNotificationAsync(Notification notification);
}
