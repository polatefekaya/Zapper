namespace realTimeApp.Client.Application.Interfaces;

public interface ICommandManagerService
{
    Task<bool> StartMessageCommands();
    Task<bool> StartGroupCommands();
    Task<bool> Start(string command);
}
