using System.Reflection.Metadata;
using Microsoft.Extensions.Logging;
using realTimeApp.Client.Application.Interfaces;

namespace realTimeApp.Client.Application.Services;

public class CommandManagerService : ICommandManagerService
{

    private readonly IMessagingCommandsService _messagingCommands;
    private readonly ILogger<CommandManagerService> _logger;

    public CommandManagerService(IMessagingCommandsService messagingCommands, ILogger<CommandManagerService> logger){
        _logger = logger;
        _messagingCommands = messagingCommands;
    }

    public Task<bool> StartGroupCommands()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> StartMessageCommands()
    {
        bool exit = false;
        while(true){
            string? line = Console.ReadLine();
            if(line is not null){
                exit = await _messagingCommands.MessageCommand(line);
            }
            if(exit) break;
        }
        return exit;
    }

    public async Task<bool> Start(string command){
        Console.Clear();
        Console.WriteLine("If you not joined a group, you can not message anybody");
        Console.WriteLine("message or group?");
        
        bool exit = false;
        switch(command){
            case "message":
                exit = await StartMessageCommands();
            break;
            case "group":
                exit = await StartGroupCommands();
            break;
            case "exit":
                exit = true;
            break;
        }
        return exit;
    }
}
