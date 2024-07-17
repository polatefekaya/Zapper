namespace realTimeApp.Client.Application.Interfaces;

public interface IMessagingCommandsService
{
    Task<bool> MessageCommand(string command);
}
