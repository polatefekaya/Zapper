using realTimeApp.Client.Domain.Data.Entities;

namespace realTimeApp.Client.Application.Interfaces.Messaging;

public interface IOnReceivedService
{
    Task OnMessageReceivedAsync(MessageEntity message);
    Task OnSecureMessageReceivedAsync(SecureMessageEntity message);
}
