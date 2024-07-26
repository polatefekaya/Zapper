using System.Text;
using realTimeApp.Client.Application.Interfaces.Messaging;
using realTimeApp.Client.Domain.Data.Entities;

namespace realTimeApp.Client.Application.Services.Messaging;

public class OnReceivedService : IOnReceivedService
{
    private readonly IMessageEncryptionService _messageEncryptionService;

    public OnReceivedService(IMessageEncryptionService messageEncryptionService){
        _messageEncryptionService = messageEncryptionService;
    }

    public async Task OnMessageReceivedAsync(MessageEntity message){
        Console.WriteLine(message.Sender + ": " + message.Body + " - " + message.sentTime);
        message.Body = await _messageEncryptionService.DecryptMessage(Encoding.UTF8.GetBytes(message.Body), "naberknkbenpolat", "polatpolatpolatp");
        Console.WriteLine(message.Sender + ": " + message.Body + " - " + message.sentTime);
        await Task.CompletedTask;
    }

    public async Task OnSecureMessageReceivedAsync(SecureMessageEntity message){
        Console.WriteLine(message.Sender + ": " + Encoding.UTF8.GetString(message.Body) + " - " + message.sentTime);
        string messageString = await _messageEncryptionService.DecryptMessage(message.Body, "naberknkbenpolat", "polatpolatpolatp");
        Console.WriteLine(message.Sender + ": " + messageString + " - " + message.sentTime);
        await Task.CompletedTask;
    }
}
