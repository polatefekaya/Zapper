using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using realTimeApp.Server.Application.Interfaces;
using realTimeApp.Server.Domain.Data.Entities;

namespace realTimeApp.Server.Application.Services;

public class HubMessageService : IHubMessageService
{
    private readonly IHubContext<HubService> _hubContext;
    private readonly ILogger<HubMessageService> _logger;

    public HubMessageService(ILogger<HubMessageService> logger, IHubContext<HubService> hubContext){
        _logger = logger;
        _hubContext = hubContext;
    }

    public async Task SendMessageToGroup(string groupName, MessageEntity message)
    {
        _logger.LogInformation("Sending a message to {gname}", groupName); 
        await _hubContext.Clients.Group(groupName).SendAsync("MessageReceived", message);
    }

    public Task SendMessageToUser(string user, MessageEntity message)
    {
        throw new NotImplementedException();
    }
}
