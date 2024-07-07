using System;
using Microsoft.AspNetCore.SignalR.Client;
namespace realTimeApp.Client.Console;

class Program 
{
    private static HubConnection _hubConnection;
    private static readonly string HostDomain = "http://localhost:5050";
    static void Main(string[] args)
    {
        System.Console.WriteLine("Hello, World!");
        
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(new Uri($"{HostDomain}/hub/notifications"))
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.StartAsync().Wait();
         Notification notification = new Notification{
                        Header = "This is a header.",
                        Text = "This is a text field."
                    };

        //_hubConnection.InvokeCoreAsync("NotifyAll", new[]{notification}).Wait();
        _hubConnection.On<Notification>(
            "NotificationReceived", OnNotificationReceivedAsync);


        System.Console.ReadKey();
    }

    private static async Task OnNotificationReceivedAsync(Notification notification)
    {
        // Do something meaningful with the notification.
        System.Console.WriteLine(notification.Header + "\n" + notification.Text);
        await Task.CompletedTask;
    }
}
