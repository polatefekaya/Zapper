using Microsoft.Extensions.Logging;
using realTimeApp.Client.Application.Interfaces;
using realTimeApp.Client.Application.Interfaces.Connection;
using realTimeApp.Client.Application.Interfaces.Messaging;
using realTimeApp.Client.Domain.Data.Entities;

namespace realTimeApp.Client.Application.Services.Messaging;

public class MessageService : IMessageService
{
    private readonly ILogger<MessageService> _logger;
    private readonly IHubConnectionService _hubConnectionService;
    private readonly IMessageEncryptionService _messageEncryptionService;

    public MessageService(ILogger<MessageService> logger, IHubConnectionService hubConnectionService, IMessageEncryptionService messageEncryptionService){
        _logger = logger;
        _hubConnectionService = hubConnectionService;
        _messageEncryptionService = messageEncryptionService;
    }

    public async Task SendMessageToGroup(string groupName, MessageEntity message)
    {
        await _hubConnectionService.InvokeAsync("SendMessageToGroup", groupName, message);
    }

    public async Task SendSecureMessageToGroup(string groupName, MessageEntity message){
        SecureMessageEntity secureMessage = new SecureMessageEntity();
        
        secureMessage.Body = await _messageEncryptionService.EncryptMessage(message.Body, "naberknkbenpolat", "polatpolatpolatp");
        secureMessage.Header = message.Header;
        secureMessage.Sender = message.Sender;
        secureMessage.sentTime = message.sentTime;

        await _hubConnectionService.InvokeAsync("SendSecureMessageToGroup", groupName, secureMessage);
    }

    public Task SendMessageToUser(string user, MessageEntity message)
    {
        throw new NotImplementedException();
    }

}
