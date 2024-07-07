using Microsoft.AspNetCore.SignalR;
using realTimeApp.Domain;

namespace realTimeApp.Application;

public class HubService : Hub, IHubService
{
    public string ConsoleWriter(){
        return "Some Text";
    }

    public override Task OnConnectedAsync()
    {
        Notification notification = new() {
            Header = "HeaderThis",
            Text = "Connected to the hub"
        };

        Task task = Clients.All.SendAsync("NotificationReceived", notification);

        return base.OnConnectedAsync();
    }

    public Task NotifyAll(Notification notification)
    {
        Task task = Clients.All.SendAsync("NotificationReceived", notification);
        return task;
    }
}
