using Microsoft.Extensions.Logging;
using realTimeApp.Client.Application.Interfaces.Connection;

namespace realTimeApp.Client.Application.Services.Connection;

public class GroupConnectionService : IGroupConnectionService
{
    private readonly ILogger<GroupConnectionService> _logger;

    public GroupConnectionService(ILogger<GroupConnectionService> logger){
        _logger = logger;
    }

    public Task JoinToGroup(string groupName)
    {
        throw new NotImplementedException();
    }

    public Task LeftFromGroup(string groupName)
    {
        throw new NotImplementedException();
    }
}
