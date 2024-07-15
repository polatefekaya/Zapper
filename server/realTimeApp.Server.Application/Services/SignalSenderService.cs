
using realTimeApp.Server.Application.Interfaces;
using realTimeApp.Server.Domain.Data.Entities;

namespace realTimeApp.Server.Application;

public class SignalSenderService : ISignalSenderService
{

    Dictionary<string, Notification> notificationDict = new Dictionary<string, Notification>{
        {"welcome", new Notification(){Header = "Welcome To SignalR", Text = "This is a demo text"}},
        {"test", new Notification(){Header = "This is a test Header", Text = "This is a test text"}}
    };

    private readonly INotificationService _notificationService;

    public SignalSenderService(INotificationService notificationService){
        _notificationService = notificationService;
    }

    public async Task SendWithKey(string key)
    {
        await _notificationService.SendNotificationAsync(notificationDict[key]);
    }
}
