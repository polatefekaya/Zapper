namespace realTimeApp.Client.Application.Interfaces.Messaging;

public interface IMessageEncryptionService
{
    Task<byte[]> EncryptMessage(string message, string key, string? iv);
    Task<string> DecryptMessage(byte[] messageBytes, string key, string? iv);
}
