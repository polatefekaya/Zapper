namespace realTimeApp.Client.Application.Interfaces.Cryptography;

public interface IEncryptionService
{
    Task<byte[]> Encrypt(string text, string key, string? iv);
    Task<string> Decrypt(byte[] textBytes, string key, string? iv);
}
