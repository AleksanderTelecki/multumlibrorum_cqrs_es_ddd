using System.Net.Mail;
using Client.Messages.Queries;
using Communication.Domain.Options;
using Communication.Domain.Services;
using Communication.Messages.Commands;
using CQRS.Communication.Abstract;
using CQRS.Communication.Enums;
using CQRS.Core.Commands.Abstract;
using Microsoft.Extensions.Options;

namespace Communication.Domain.CommandHandlers;

public class CommunicationCommandHandlers:
    ICommandHandler<SendReceiptEmailCommand>
{
    
    private readonly IRestDispatcher _restDispatcher;
    private readonly MailService _mailService;

    public CommunicationCommandHandlers(IRestDispatcher restDispatcher, MailService mailService)
    {
        _restDispatcher = restDispatcher;
        _mailService = mailService;
    }


    public async Task Handle(SendReceiptEmailCommand command, CancellationToken cancellation)
    {
        var client = await _restDispatcher.DispatchQuery(new GetClientDetailsQuery() { ClientId = command.ClientId },
            EndpointEnum.ClientEndpoint);
        
        string htmlEmailBodyTemplate = @"
<!DOCTYPE html>
<html>
<head>
    <style>
        body { font-family: Arial, sans-serif; line-height: 1.6; }
        .container { width: 80%; margin: 0 auto; padding: 20px; }
        h1 { color: #333; }
        p { color: #555; }
    </style>
</head>
<body>
    <div class='container'>
        <h1>Receipt for Your Purchase</h1>
        <p>Dear Customer,</p>
        <p>Thank you for your recent purchase. We appreciate your business and hope you are satisfied with your purchase.</p>
        <p>Please find attached the receipt for your transaction. The receipt includes a detailed summary of your purchase. We recommend keeping this document for your records.</p>
        <p>If you have any questions or require further assistance, please do not hesitate to contact us at [Your Contact Information].</p>
        <p>Thank you again for choosing us. We look forward to serving you in the future.</p>
        <p>Warm regards,</p>
        <p>Multum Librorum</p>
    </div>
</body>
</html>";

        
        _mailService.CreateMail(new []{ client.Email}, $"Receipt for order: {command.OrderId}", htmlEmailBodyTemplate, new Attachment[]{new Attachment(command.AttachmentFilePath)} );
    }
}