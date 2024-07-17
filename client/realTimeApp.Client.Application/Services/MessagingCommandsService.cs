using System.Reflection.Metadata;
using realTimeApp.Client.Application.Interfaces;
using realTimeApp.Client.Domain.Data.Entities;

namespace realTimeApp.Client.Application.Services;

public class MessagingCommandsService : IMessagingCommandsService
{
    private readonly IMessageService _messageService;

    public MessagingCommandsService(IMessageService messageService){
        _messageService = messageService;
    }
    public async Task<int> MessageCommand(string command)
    {
        int exitType = 0;
        Console.Clear();
        Console.WriteLine("group or user?");
        //Classify Commands
        switch(command){
            case "group":
                break;
            case "user":
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

    private async Task SendMessageToGroup(string groupName,string command){
        while(true){
            switch(command){
                //Implement
            }
        }
        MessageEntity message = new MessageEntity{
            Body = command
        };
        await _messageService.SendMessageToGroup(groupName, message);
    }
}
