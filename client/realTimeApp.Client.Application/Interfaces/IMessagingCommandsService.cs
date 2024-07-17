namespace realTimeApp.Client.Application.Interfaces;

public interface IMessagingCommandsService
{
    Task<int> MessageCommand(string command);
}
