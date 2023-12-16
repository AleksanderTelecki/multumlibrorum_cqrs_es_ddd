using Microsoft.EntityFrameworkCore;
using Payment.Domain.Repository.Entity;

namespace Payment.Domain;

public class RepaymentsDataContext: DbContext
{
    private readonly DbContextOptions _options;

    public RepaymentsDataContext(DbContextOptions options) : base(options)
    {
        _options = options;
    }
    
    public DbSet<RepaymentEntity> Repayments { get; set; }
}