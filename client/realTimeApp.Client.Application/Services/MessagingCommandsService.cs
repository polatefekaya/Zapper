using System.Reflection.Metadata;
using realTimeApp.Client.Application.Interfaces;
using realTimeApp.Client.Domain.Data.Entities;

namespace realTimeApp.Client.Application.Services;

public class MessagingCommandsService : IMessagingCommandsService
{
    private readonly IMessageService _messageService;
    private readonly ISessionManagerService _sessionManagerService;

    public MessagingCommandsService(IMessageService messageService, ISessionManagerService sessionManagerService){
        _messageService = messageService;
        _sessionManagerService = sessionManagerService;
    }
    public async Task<int> MessageCommand(string command)
    {
        int exitType = 0;
        Console.Clear();
        Console.WriteLine("group or user?");
        //Classify Commands
        switch(command){
            case "group":
                exitType = await SendMessageToGroup();
                break;
            case "user":
                exitType = 2;
                break;
            case "exit":
                exitType = 1;
                break;
            case "..":
                exitType = 2;
                break;
        }
        return exitType;
    }

    public async Task<int> SendMessageToGroup(){
        string? sessionName = _sessionManagerService.GetCurrentSession();
        ArgumentNullException.ThrowIfNullOrWhiteSpace(sessionName);

        int exitType = 0;
        while(true){
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
                        MessageEntity message = new MessageEntity{
                            Body = line
                        };
                        await SendMessage(sessionName, message);
                    break;
                }
                if(exitType == 1 || exitType == 2) break;
            }
        }
        return exitType;
    }

    public async Task SendMessage(string identifier, MessageEntity message){
        await _messageService.SendMessageToGroup(identifier, message);
    }
}
