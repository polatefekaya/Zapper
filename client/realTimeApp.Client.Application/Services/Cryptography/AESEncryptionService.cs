using System.Security.Cryptography;
using realTimeApp.Client.Application.Interfaces.Cryptography;
using System.Text;

namespace realTimeApp.Client.Application.Services.Cryptography;

public class AESEncryptionService : IEncryptionService
{
    public async Task<string> Decrypt(byte[] textBytes, string key, string? iv = null)
    {
        byte[]? decryptedTextBytes = null;

        using(Aes aes = Aes.Create()){
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            if(iv is null){
                aes.GenerateIV();
            } else {
                aes.IV = Encoding.UTF8.GetBytes(iv);
            }

            using (ICryptoTransform decryptor = aes.CreateDecryptor()){
                decryptedTextBytes = decryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
            }
        }
        string? decryptedTextString = Encoding.UTF8.GetString(decryptedTextBytes);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(decryptedTextString);
        
        return decryptedTextString;
    }

    public async Task<byte[]> Encrypt(string text, string key, string? iv = null)
    {
        byte[]? encryptedTextBytes = null;
        byte[] textBytes = Encoding.UTF8.GetBytes(text);

        using(Aes aes = Aes.Create()){
            aes.Key = Encoding.UTF8.GetBytes(key);;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            if(iv is null){
                aes.GenerateIV();
            } else {
                aes.IV = Encoding.UTF8.GetBytes(iv);
            }
            
            using (ICryptoTransform encryptor = aes.CreateEncryptor()){
                encryptedTextBytes = encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
            }
        }

        return encryptedTextBytes;
    }
}
