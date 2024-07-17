namespace realTimeApp.Client.Application.Interfaces;

public interface IGroupCommandsService
{
    Task<int> GroupCommand(string command);
}
