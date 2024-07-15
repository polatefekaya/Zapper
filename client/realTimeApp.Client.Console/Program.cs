using realTimeApp.Client.Domain.Data.Entities;
using Microsoft.AspNetCore.SignalR.Client;
using realTimeApp.Client.Application.Interfaces;
using realTimeApp.Client.Application.Services;
namespace realTimeApp.Client.Console;

class Program 
{
    static Dictionary<string, Notification> notificationDict = new Dictionary<string, Notification>{
        {"welcome", new Notification(){Header = "Welcome To SignalR", Text = "This is a demo text"}},
        {"test", new Notification(){Header = "This is a test Header", Text = "This is a test text"}}
    };

    static void Main(string[] args)
    {
        System.Console.WriteLine("Hello, World!");
        
        IHubConnectionService hubConnectionService = new HubConnectionService();

        hubConnectionService.BuildConnection();
        hubConnectionService.StartConnection();

         Notification notification = new Notification{
                        Header = "This is a header.",
                        Text = "This is a text field."
                    };

        //_hubConnection.InvokeCoreAsync("NotifyAll", new[]{notification}).Wait();
        hubConnectionService.On<Notification>("NotificationReceived", OnNotificationReceivedAsync);

        while(true){
            string? line = System.Console.ReadLine();
        }
    }

    private static async Task OnNotificationReceivedAsync(Notification notification)
    {
        // Do something meaningful with the notification.
        System.Console.WriteLine(notification.Header + "\n" + notification.Text);
        await Task.CompletedTask;
    }
}
