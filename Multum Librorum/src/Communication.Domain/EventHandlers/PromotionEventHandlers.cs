using System.Net.Mail;
using System.Text;
using Client.Messages.Queries;
using Communication.Domain.Services;
using CQRS.Communication.Abstract;
using CQRS.Communication.Enums;
using CQRS.Core.Events.Abstract;
using Promotion.Messages.Events;
using Promotion.Messages.Models;
using Promotion.Messages.Queries;

namespace Communication.Domain.EventHandlers;

public class PromotionEventHandlers:
    IEventHandler<PromotionAddedEvent>
{
    
    private readonly MailService _mailService;
    private readonly IRestDispatcher _restDispatcher;

    public PromotionEventHandlers(MailService mailService, IRestDispatcher restDispatcher)
    {
        _mailService = mailService;
        _restDispatcher = restDispatcher;
    }

    public async Task Handle(PromotionAddedEvent @event, CancellationToken cancellation)
    {
        var promotion = await _restDispatcher.DispatchQuery(new GetPromotionQuery() { PromotionId = @event.Id },
            EndpointEnum.PromotionEndpoint);

        var clients = await _restDispatcher.DispatchQuery(new GetClientsQuery(), EndpointEnum.ClientEndpoint);


        _mailService.CreateMail(clients.Select(x => x.Email), "New Promotion on MultumLibrorum",
            CreatePromotionEmailBody(promotion), new Attachment[]{});
    }
    
    private string CreatePromotionEmailBody(PromotionModel promotion)
    {
        
        string htmlTemplate = @"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
        }
        .container {
            width: 80%;
            margin: 0 auto;
            padding: 20px;
        }
        h1, h2 {
            color: #333;
        }
        ul {
            list-style-type: none;
            padding: 0;
        }
        li {
            margin-bottom: 10px;
        }
        .promotion-details {
            background-color: #f2f2f2;
            padding: 10px;
            border-radius: 5px;
        }
    </style>
</head>
<body>
    <div class='container'>
        <h1>Exciting Promotion!</h1>
        <div class='promotion-details'>
            <h2>Promotion Details:</h2>
            <p><strong>Description:</strong> {Description}</p>
            <p><strong>Discount:</strong> {PromotionInPercentage}%</p>
            <p><strong>Start Date:</strong> {Regdate}</p>
            <p><strong>End Date:</strong> {EndDate}</p>
        </div>
        <h2>Featured Products:</h2>
        <ul>{ProductList}</ul>
    </div>
</body>
</html>";
        
        StringBuilder sb = new StringBuilder(htmlTemplate);

        // Replace placeholders with actual promotion data
        sb.Replace("{Description}", promotion.Description)
            .Replace("{PromotionInPercentage}", promotion.PromotionInPercentage.ToString())
            .Replace("{Regdate}", promotion.Regdate.ToString("yyyy-MM-dd"))
            .Replace("{EndDate}", promotion.EndDate.ToString("yyyy-MM-dd"));

        // Build the products list
        StringBuilder productsListBuilder = new StringBuilder();
        foreach (var product in promotion.Products)
        {
            productsListBuilder.AppendFormat("<li>{0}</li>", product.ProductName);
        }

        // Replace the products placeholder with actual products list
        sb.Replace("{ProductList}", productsListBuilder.ToString());

        return sb.ToString();
    }
    
}