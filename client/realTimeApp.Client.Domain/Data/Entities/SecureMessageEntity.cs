namespace realTimeApp.Client.Domain.Data.Entities;

public class SecureMessageEntity
{
    public string Sender {get; set;} = string.Empty;
    public byte[]? Body {get; set;} = null;
    public DateTime sentTime {get; set;}
    public string? Header {get; set;} = null;
}
