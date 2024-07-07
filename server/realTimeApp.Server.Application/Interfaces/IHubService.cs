using realTimeApp.Server.Domain;

namespace realTimeApp.Server.Application;

public interface IHubService
{
    string ConsoleWriter();
    Task NotifyAll(Notification notification);
}
