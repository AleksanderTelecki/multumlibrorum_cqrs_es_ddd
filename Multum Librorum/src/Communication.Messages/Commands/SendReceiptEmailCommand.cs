using CQRS.Core.Commands.Abstract;

namespace Communication.Messages.Commands;

public class SendReceiptEmailCommand: ICommand
{
    public Guid ClientId { get; set; }
    public Guid OrderId { get; set; }
    public string AttachmentFilePath { get; set; }
}