using realTimeApp.Client.Domain.Data.Entities;

namespace realTimeApp.Client.Application.Interfaces;

public interface IMessageService
{
    Task SendMessageToGroup(string groupName, MessageEntity message);
    Task SendMessageToUser(string user, MessageEntity message);
}
