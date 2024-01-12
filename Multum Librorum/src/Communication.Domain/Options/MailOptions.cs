namespace Communication.Domain.Options;

public class MailOptions
{
    public List<string> Whitelist { get; set; } 
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; }
    public string SmtpPassword { get; set; }
    public string FromAddress { get; set; }
}