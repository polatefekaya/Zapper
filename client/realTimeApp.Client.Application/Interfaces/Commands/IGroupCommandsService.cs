namespace realTimeApp.Client.Application.Interfaces.Commands;

public interface IGroupCommandsService
{
    Task<int> GroupCommand(string command);
}
