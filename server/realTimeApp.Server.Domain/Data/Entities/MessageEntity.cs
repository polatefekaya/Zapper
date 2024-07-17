namespace realTimeApp.Server.Domain.Data.Entities;


public class MessageEntity
{
    public string Sender {get; set;} = string.Empty;
    public string Body {get; set;} = string.Empty;
    public DateTime sentTime {get; set;}
    public string? Header {get; set;} = null;
}
