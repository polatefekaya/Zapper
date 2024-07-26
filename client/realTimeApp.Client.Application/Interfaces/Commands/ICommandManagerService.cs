namespace realTimeApp.Client.Application.Interfaces.Commands;

public interface ICommandManagerService
{
    Task<int> StartMessageCommands();
    Task<int> StartGroupCommands();
    Task<int> Start(string command);
    bool IsSessionOkay();
    void SetSession(string session);
    void SetSessionType(string type);
}
