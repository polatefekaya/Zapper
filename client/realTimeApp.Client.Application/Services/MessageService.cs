using Microsoft.Extensions.Logging;
using realTimeApp.Client.Application.Interfaces;
using realTimeApp.Client.Domain.Data.Entities;

namespace realTimeApp.Client.Application.Services;

public class MessageService : IMessageService
{
    private readonly ILogger<MessageService> _logger;
    private readonly IHubConnectionService _hubConnectionService;

    public MessageService(ILogger<MessageService> logger, IHubConnectionService hubConnectionService){
        _logger = logger;
        _hubConnectionService = hubConnectionService;
    }

    public async Task SendMessageToGroup(string groupName, MessageEntity message)
    { 
        await _hubConnectionService.InvokeAsync("SendMessageToGroup", groupName, message);
    }

    public Task SendMessageToUser(string user, MessageEntity message)
    {
        throw new NotImplementedException();
    }
}
