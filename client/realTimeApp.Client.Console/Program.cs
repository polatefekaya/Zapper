using realTimeApp.Client.Domain.Data.Entities;
using Microsoft.AspNetCore.SignalR.Client;
namespace realTimeApp.Client.Console;

class Program 
{
    private static HubConnection _hubConnection;
    private static readonly string HostDomain = "http://localhost:5050";

    static Dictionary<string, Notification> notificationDict = new Dictionary<string, Notification>{
        {"welcome", new Notification(){Header = "Welcome To SignalR", Text = "This is a demo text"}},
        {"test", new Notification(){Header = "This is a test Header", Text = "This is a test text"}}
    };

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

        while (true){
            Commands().Wait();
        }
    }

    private static async Task OnNotificationReceivedAsync(Notification notification)
    {
        // Do something meaningful with the notification.
        System.Console.WriteLine(notification.Header + "\n" + notification.Text);
        await Task.CompletedTask;
    }

    public static async Task Commands(){
        string? line = System.Console.ReadLine();
        if(line is not null) {
            bool ContainsKey = notificationDict.ContainsKey(line.ToLower());
            if(ContainsKey){
                Notification notification = notificationDict[line];

                await _hubConnection.SendAsync("NotifyAll", notification);
            }
        }
    }
}
