
using realTimeApp.Client.Application.Interfaces.Cryptography;
using realTimeApp.Client.Application.Interfaces.Messaging;
using realTimeApp.Client.Application.Services.Cryptography;

namespace realTimeApp.Client.Application.Services;

public class MessageEncryptionService : IMessageEncryptionService
{
    private readonly IEncryptionService _encryptionService;

    public MessageEncryptionService(){
        _encryptionService = new AESEncryptionService();
    }

    public async Task<string> DecryptMessage(byte[] messageBytes, string key, string? iv = null)
    {
        return await _encryptionService.Decrypt(messageBytes, key, iv);
    }

    public async Task<byte[]> EncryptMessage(string message, string key, string? iv = null)
    {
        return await _encryptionService.Encrypt(message, key, iv);
    }
}
