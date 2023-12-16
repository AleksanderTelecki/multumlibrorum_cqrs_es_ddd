using Payment.Domain.Repository.Entity;

namespace Payment.Domain.Repository;

public interface IRepaymentRepository
{
    public Task CreateNewRepayment(Guid clientId);
    public Task UpdateRepayment(RepaymentEntity repaymentEntity);
}

public class RepaymentRepository: IRepaymentRepository
{
    private readonly RepaymentsDataContext _repaymentsDataContext;
    
    public RepaymentRepository(RepaymentsDataContext repaymentsDataContext)
    {
        _repaymentsDataContext = repaymentsDataContext;
    }
    
    public Task CreateNewRepayment(Guid clientId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRepayment(RepaymentEntity repaymentEntity)
    {
        throw new NotImplementedException();
    }
}