using Microsoft.EntityFrameworkCore;
using Payment.Domain.Repository.Entity;
using Payment.Messages.Models;

namespace Payment.Domain.Repository;

public interface IRepaymentRepository
{
    public Task CreateNewRepayment(RepaymentEntity repaymentEntity);
    public Task UpdateStatus(Guid repaymentId, RepaymentStatus repaymentStatus);
}

public class RepaymentRepository: IRepaymentRepository
{
    private readonly RepaymentsDataContext _repaymentsDataContext;
    
    public RepaymentRepository(RepaymentsDataContext repaymentsDataContext)
    {
        _repaymentsDataContext = repaymentsDataContext;
    }
    
    public async Task CreateNewRepayment(RepaymentEntity repaymentEntity)
    {
        _repaymentsDataContext.Repayments.Add(repaymentEntity);
        await _repaymentsDataContext.SaveChangesAsync();
    }

    public async Task UpdateStatus(Guid repaymentId, RepaymentStatus repaymentStatus)
    {
        var repayment = await _repaymentsDataContext.Repayments.FirstAsync(x => x.Id == repaymentId);
        repayment.Status = repaymentStatus;
        
        await _repaymentsDataContext.SaveChangesAsync();
    }
}