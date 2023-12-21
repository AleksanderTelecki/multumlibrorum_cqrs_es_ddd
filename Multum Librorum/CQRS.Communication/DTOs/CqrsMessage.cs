namespace CQRS.Communication.DTOs;

public class CqrsMessage
{
    public string Type { get; set; }
    public string Data { get; set; }

    public CqrsMessage()
    {
        
    }

    public CqrsMessage(string type, string data)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentNullException(nameof(type));
        
        Type = type;
        Data = data;
    }
}