namespace realTimeApp.Client.Application.Interfaces.Connection;

public interface IGroupConnectionService
{
    Task JoinToGroup(string groupName);
    Task LeftFromGroup(string groupName);
}
