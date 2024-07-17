using System.Reflection.Metadata;
using Microsoft.Extensions.Logging;
using realTimeApp.Client.Application.Interfaces;

namespace realTimeApp.Client.Application.Services;

public class CommandManagerService : ICommandManagerService
{

    private readonly IMessagingCommandsService _messagingCommands;
    private readonly IGroupCommandsService _groupCommands;
    private readonly ILogger<CommandManagerService> _logger;
    private readonly ISessionManagerService _sessionManager;

    public CommandManagerService(IMessagingCommandsService messagingCommands, ILogger<CommandManagerService> logger, IGroupCommandsService groupCommands, ISessionManagerService sessionManager){
        _logger = logger;
        _messagingCommands = messagingCommands;
        _groupCommands = groupCommands;
        _sessionManager = sessionManager;
    }

    public async Task<int> StartGroupCommands()
    {
        Console.WriteLine("You are in group commands, you can join new group sessions");
        Console.WriteLine("list, join");
        int exitType = 0;
        while(true){
            string? line = Console.ReadLine();
            if(line is not null){
                exitType = await _groupCommands.GroupCommand(line);
            }

            if(exitType == 1 || exitType == 2) break;
        }
        return exitType;
    }

    public async Task<int> StartMessageCommands()
    {
        int exitType = 0;
        while(true){
            string? line = Console.ReadLine();
            if(line is not null){
                exitType = await _messagingCommands.MessageCommand(line);
            }
            if(exitType == 1) break;
        }
        return exitType;
    }

    public async Task<int> Start(string command){
        Console.Clear();

        if(_sessionManager.GetCurrentSession() is not null){
            Console.WriteLine($"Session: {_sessionManager.GetCurrentSession()}");
        }

        if(_sessionManager.GetCurrentSessionType() is not null){
            Console.WriteLine($"Session Type: {_sessionManager.GetCurrentSessionType()}");
        }

        int exitType = 0;
        switch(command){
            case "message":
                if(!IsSessionOkay()) break;

                exitType = await StartMessageCommands();
            break;
            case "group":
                exitType = await StartGroupCommands();
            break;
            case "exit":
                exitType = 1;
            break;
        }
        return exitType;
    }

    public bool IsSessionOkay(){
        if(_sessionManager.GetCurrentSession() is null || _sessionManager.GetCurrentSessionType() is null){
            _logger.LogError("You don't belong to anyone, who is the receiver for your messages?");
            return false;
        }
        return true;
    }

    public void SetSession(string session){

    }
    public void SetSessionType(string type){

    }
}
