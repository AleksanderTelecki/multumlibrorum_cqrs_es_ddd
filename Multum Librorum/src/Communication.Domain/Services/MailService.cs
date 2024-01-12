using System.Net;
using System.Net.Mail;
using Communication.Domain.Options;
using Microsoft.Extensions.Options;

namespace Communication.Domain.Services;

public class MailService
{
    private readonly IOptions<MailOptions> _options;
    
    public MailService(IOptions<MailOptions> options)
    {
        _options = options;
    }

    public void CreateMail(IEnumerable<string> recipients, string title, string body, IEnumerable<Attachment> attachments)
    {
        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(_options.Value.FromAddress);

            if (_options.Value.Whitelist.Any())
            {
                foreach (var recipient in _options.Value.Whitelist)
                {
                    mail.To.Add(new MailAddress(recipient));
                }
            }
            else
            {
                foreach (var recipient in recipients)
                {
                    mail.To.Add(new MailAddress(recipient));
                } 
            }
            
            mail.Subject = title;
            mail.Body = body;
            mail.IsBodyHtml = true;

            foreach (var attachment in attachments)
            {
                mail.Attachments.Add(attachment);
            }

            using (SmtpClient smtpClient = new SmtpClient(_options.Value.SmtpHost, _options.Value.SmtpPort))
            {
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(_options.Value.SmtpUsername, _options.Value.SmtpPassword);

                try
                {
                    smtpClient.Send(mail);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred: " + ex.Message);
                }
            }
        }
    }
    
}