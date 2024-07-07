using Microsoft.AspNetCore.SignalR;
using realTimeApp.Server.Domain.Data.Entities;
using realTimeApp.Server.Application.Interfaces;

namespace realTimeApp.Server.Application.Services;

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
