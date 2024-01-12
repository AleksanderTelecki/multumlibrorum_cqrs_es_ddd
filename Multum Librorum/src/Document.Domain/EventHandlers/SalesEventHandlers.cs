using System.Text;
using Communication.Messages.Commands;
using CQRS.Communication.Abstract;
using CQRS.Communication.Enums;
using CQRS.Core.Events.Abstract;
using Document.Domain.Options;
using Document.Domain.Repository;
using Document.Domain.Repository.Entities;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Options;
using Sales.Messages.Events;
using Sales.Messages.Models;
using Sales.Messages.Queries;

namespace Document.Domain.EventHandlers;

public class SalesEventHandlers :
    IEventHandler<OrderPaidEvent>
{
    private readonly IOptions<FileRepoOptions> _fileRepoOptions;
    private readonly IDocumentRepository _documentRepository;
    private readonly IRestDispatcher _restDispatcher;

    public SalesEventHandlers(IOptions<FileRepoOptions> fileRepoOptions, IDocumentRepository documentRepository, IRestDispatcher restDispatcher)
    {
        _fileRepoOptions = fileRepoOptions;
        _documentRepository = documentRepository;
        _restDispatcher = restDispatcher;
    }
    
    public async Task Handle(OrderPaidEvent @event, CancellationToken cancellation)
    {
        var orderDetails =
            await _restDispatcher.DispatchQuery(new GetOrderDetailsByOrderIdQuery() { OrderId = @event.Id },
                EndpointEnum.SalesEndpoint);
        
        var path = ConvertHtmlToPdf(GenerateReceiptHtml(orderDetails), _fileRepoOptions.Value.PathToFileRepo+$@"\receipt-{@event.Id}.pdf");
        
        var fileEntity = new FileEntity(@event.Id, path, DateTime.Now);

        await _documentRepository.CreateFile(fileEntity);

        await _restDispatcher.DispatchCommand(
            new SendReceiptEmailCommand()
                { OrderId = orderDetails.OrderId, ClientId = orderDetails.ClientId, AttachmentFilePath = path },
            EndpointEnum.CommunicationEndpoint);
    }
    
    private string GenerateReceiptHtml(OrderModel order)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html>");
        sb.AppendLine("<head>");
        sb.AppendLine("    <title>Receipt</title>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body>");
        sb.AppendLine("    <div class='container'>");
        sb.AppendLine("        <h1>MultumLibrorum</h1>");
        sb.AppendLine("        <h2>Receipt</h2>");
        sb.AppendLine($"       <h3>Order ID: {order.OrderId}</h3>");
        sb.AppendLine("        <table>");
        sb.AppendLine("            <tr><th>Product</th><th>Quantity</th><th>Price</th></tr>");

        decimal total = 0;
        foreach (var item in order.Items)
        {
            sb.AppendLine($"        <tr><td>{item.ProductName}</td><td>{item.Quantity}</td><td>${item.ProductPrice}</td></tr>");
            total += item.ProductPrice * item.Quantity;
        }

        sb.AppendLine($"       <tr><td colspan='2' class='total'>Total</td><td>${total}</td></tr>");
        sb.AppendLine("        </table>");
        sb.AppendLine("    </div>");
        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }
    
    private string ConvertHtmlToPdf(string htmlString, string outputPath)
    {
        using (iTextSharp.text.Document document = new iTextSharp.text.Document())
        {
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));
            document.Open();
            using (StringReader sr = new StringReader(htmlString))
            {
                HTMLWorker htmlWorker = new HTMLWorker(document);
                htmlWorker.Parse(sr);
            }
            document.Close();
        }

        return outputPath;
    }
    
}