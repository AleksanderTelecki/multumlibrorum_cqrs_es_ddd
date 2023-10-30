using Microsoft.EntityFrameworkCore;
using Promotion.Domain.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Domain
{
    public class PromotionDomainDataContext : DbContext
    {
        private readonly DbContextOptions _options;
        public PromotionDomainDataContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        public DbSet<PromotionEntity> Promotions { get; set; }
        public DbSet<PromotionProductEntity> PromotionProducts { get; set; }
    }
}
