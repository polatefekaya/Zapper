using realTimeApp.Client.Domain.Data.Entities;

namespace realTimeApp.Client.Application.Interfaces;

public interface IMessagingCommandsService
{
    Task<int> MessageCommand(string command);
    Task<int> SendMessageToGroup();
    Task SendMessage(string identifier, MessageEntity message);
}
