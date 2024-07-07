using realTimeApp.Domain;

namespace realTimeApp.Application;

public interface IHubService
{
    string ConsoleWriter();
    Task NotifyAll(Notification notification);
}
