using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using realTimeApp.Domain.Data.Entities;
using realTimeApp.Server.Application.Interfaces;
using realTimeApp.Server.Domain.Data.Entities;

namespace realTimeApp.Server.Application.Services;

public class HubMessageService : IHubMessageService
{
    private readonly IHubContext<HubService> _hubContext;
    private readonly ILogger<HubMessageService> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public HubMessageService(ILogger<HubMessageService> logger, IHubContext<HubService> hubContext, IPublishEndpoint publishEndpoint){
        _logger = logger;
        _hubContext = hubContext;
        _publishEndpoint = publishEndpoint;
    }

    public async Task SendMessageToGroup(string groupName, MessageEntity message)
    {
        _logger.LogInformation("Sending a message to {gname}", groupName); 
        await _hubContext.Clients.Group(groupName).SendAsync("MessageReceived", message);
    }

    public async Task SendSecureMessageToGroup(string groupName, SecureMessageEntity message){
        _logger.LogInformation("Sending a secure message to {gname}", groupName);
        
        await _publishEndpoint.Publish(message);

        await _hubContext.Clients.Group(groupName).SendAsync("SecureMessageReceived", message);
    }

    public Task SendMessageToUser(string user, MessageEntity message)
    {
        throw new NotImplementedException();
    }
}
