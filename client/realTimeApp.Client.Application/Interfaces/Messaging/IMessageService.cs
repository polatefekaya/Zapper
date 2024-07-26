using realTimeApp.Client.Domain.Data.Entities;

namespace realTimeApp.Client.Application.Interfaces.Messaging;

public interface IMessageService
{
    Task SendMessageToGroup(string groupName, MessageEntity message);
    Task SendSecureMessageToGroup(string groupName, MessageEntity message);
    Task SendMessageToUser(string user, MessageEntity message);
}
