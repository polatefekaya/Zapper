namespace realTimeApp.Client.Application.Interfaces;

public interface ICommandManagerService
{
    Task<int> StartMessageCommands();
    Task<int> StartGroupCommands();
    Task<int> Start(string command);
    bool IsSessionOkay();
    void SetSession(string session);
    void SetSessionType(string type);
}
