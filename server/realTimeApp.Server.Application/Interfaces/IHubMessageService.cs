using realTimeApp.Server.Domain.Data.Entities;

namespace realTimeApp.Server.Application.Interfaces;

public interface IHubMessageService
{
    Task SendMessageToGroup(string groupName, MessageEntity message);
    Task SendSecureMessageToGroup(string groupName, SecureMessageEntity message);
    Task SendMessageToUser(string user, MessageEntity message);
}
