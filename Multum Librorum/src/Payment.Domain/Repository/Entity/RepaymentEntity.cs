using System.ComponentModel.DataAnnotations.Schema;
using Payment.Messages.Models;

namespace Payment.Domain.Repository.Entity;

[Table("Repayments", Schema = "Repayment")]
public class RepaymentEntity
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public RepaymentStatus Status { get; set; }
    [Column(TypeName = "decimal(6, 2)")]
    public decimal Amount { get; set; }
    public string CardNumber { get; set; }
    public string CardOwner { get; set; }
    public string ExpirationDate { get; set; }
    public string CVV { get; set; }
    public DateTime RegDate { get; set; }

    public RepaymentEntity()
    {
        
    }
    
    public RepaymentEntity(Guid id, Guid orderId, decimal amount, string cardNumber, string cardOwner, string expirationDate, string cvv, DateTime regDate, RepaymentStatus repaymentStatus)
    {
        Id = id;
        OrderId = orderId;
        Amount = amount;
        CardNumber = cardNumber;
        CardOwner = cardOwner;
        ExpirationDate = expirationDate;
        CVV = cvv;
        Status = repaymentStatus;
        RegDate = regDate;
    }
    
    
    
}