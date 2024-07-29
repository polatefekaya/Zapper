namespace realTimeApp.Domain.Data.Entities;

public class SecureMessageEntity
{
    public string Sender {get; set;} = string.Empty;
    public byte[]? Body {get; set;} = null;
    public string? Header {get; set;} = null;
    public DateTime sentTime {get; set;}
    public DateTime seenTime {get; set;}
    public string? HMAC {get; set;}
    public string? UserIdHash {get; set;}
    public string? ChatIdHash {get; set;}
    public string? ReceiverUserIdHash {get; set;}
}

