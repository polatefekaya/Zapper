using Microsoft.Extensions.Logging;
using realTimeApp.Client.Application.Interfaces;

namespace realTimeApp.Client.Application.Services;

public class GroupCommandsService : IGroupCommandsService
{
    private readonly IHubConnectionService _hubConnectionService;
    private readonly ISessionManagerService _sessionManagerService;
    private readonly ILogger<GroupCommandsService> _logger;
    public GroupCommandsService(IHubConnectionService hubConnectionService, ISessionManagerService sessionManagerService, ILogger<GroupCommandsService> logger){
        _hubConnectionService = hubConnectionService;
        _sessionManagerService = sessionManagerService;
        _logger = logger;
    }
    public async Task<int> GroupCommand(string command)
    {
        switch(command){
            case "exit":
                return 1;
            case "..":
                return 2;
            case "join":
                return await JoinCommand();
            default:
                return 0;
        }
    }

    public async Task<int> JoinCommand(){
        Console.Clear();
        Console.WriteLine("Type name of the group you want to join");

        int exitType = 0;

        string? line = Console.ReadLine();
        if(line is not null){
            switch(line){
                case "exit":
                    exitType = 1;
                    break;
                case "..":
                    exitType = 2;
                    break;
                default:
                    await _hubConnectionService.InvokeAsync("AddToGroup", line);
                    _logger.LogInformation("Setting Session Info");
                    _sessionManagerService.SetCurrentSession(line.ToString());
                    _sessionManagerService.SetCurrentSessionType("Group");
                    Console.Clear();
                    Console.WriteLine("Joined");
                    exitType = 2;
                break;
            }
        }
        return exitType;
    }
}
