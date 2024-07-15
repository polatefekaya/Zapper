namespace realTimeApp.Server.Application;

public interface ISignalSenderService
{
    Task SendWithKey(string key);
}
