﻿using Microsoft.AspNetCore.SignalR.Client;
using realTimeApp.Client.Domain.Data.Entities;

namespace realTimeApp.Client.Application.Interfaces;

public interface IHubConnectionService
{
    Task<HubConnection> BuildConnection(string? hubPath = null);
    Task<HubConnection> StartConnection();
    Task<IDisposable> On<T>(string listeningName, Func<T, Task> handler);
    ref HubConnection GetHubConnection();
    Task InvokeAsync(string methodName, object argument, object? argument2 = null);
    Task SendAsync(string methodName, object argument);
}
