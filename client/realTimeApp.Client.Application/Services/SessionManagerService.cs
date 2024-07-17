using realTimeApp.Client.Application.Interfaces;

namespace realTimeApp.Client.Application.Services;

public class SessionManagerService : ISessionManagerService
{
    private string? _currentSession;
    private string? _currentSessionType;

    public SessionManagerService(){

    }

    public void SetCurrentSession(string session){
        _currentSession = session;
    }

    public string? GetCurrentSession(){
        return _currentSession;
    }
    public void SetCurrentSessionType(string type){
        _currentSessionType = type;
    }
    public string? GetCurrentSessionType(){
        return _currentSessionType;
    }
}
