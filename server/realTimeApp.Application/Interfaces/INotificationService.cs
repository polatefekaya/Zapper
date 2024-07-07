using realTimeApp.Domain;

namespace realTimeApp.Application;

public interface INotificationService
{
    Task SendNotificationAsync(Notification notification);
}
