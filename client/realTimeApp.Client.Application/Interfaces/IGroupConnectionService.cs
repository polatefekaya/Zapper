namespace realTimeApp.Client.Application.Interfaces;

public interface IGroupConnectionService
{
    Task JoinToGroup(string groupName);
    Task LeftFromGroup(string groupName);
}
