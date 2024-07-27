using System.Text;

namespace realTimeApp.Client.Domain.Data.Entities;

public class MessageEntity
{
    public string Sender {get; set;} = string.Empty;
    public string Body {get; set;} = string.Empty;
    public DateTime sentTime {get; set;}
    public string? Header {get; set;} = null;

    /// <summary>
    /// Empty Secure Message means there is no body associated with message. You have to manually encrypt the body and assign it to the Secure Message Entity
    /// </summary>
    /// <returns>Secure Message Entity</returns>
    public SecureMessageEntity ToEmptySecureMessage(){
        return new SecureMessageEntity{
            Sender = this.Sender,
            sentTime = this.sentTime,
            Header = this.Header
        };
    }
}
