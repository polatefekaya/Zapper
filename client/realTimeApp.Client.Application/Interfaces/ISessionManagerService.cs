namespace realTimeApp.Client.Application.Interfaces;

public interface ISessionManagerService
{
    void SetCurrentSession(string session);
    void SetCurrentSessionType(string type);
    string? GetCurrentSession();
    string? GetCurrentSessionType();
}
