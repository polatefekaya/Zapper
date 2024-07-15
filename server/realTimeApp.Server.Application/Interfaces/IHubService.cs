using realTimeApp.Server.Domain.Data.Entities;

namespace realTimeApp.Server.Application.Interfaces;

public interface IHubService
{
    string ConsoleWriter();
    Task NotifyAll(Notification notification);
    
    
}
