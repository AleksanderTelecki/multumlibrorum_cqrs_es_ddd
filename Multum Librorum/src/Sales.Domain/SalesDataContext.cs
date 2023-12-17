using Microsoft.EntityFrameworkCore;
using Sales.Domain.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain
{
    public class SalesDataContext: DbContext
    {
        private readonly DbContextOptions _options;

        public SalesDataContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        public DbSet<CartEntity> Carts { get; init; }

        public DbSet<CartItemEntity> CartItems { get; init; }
        
        public DbSet<OrderEntity> Orders { get; init; }
        
        public DbSet<OrderItemEntity> OrderItems { get; init; }
    }
}
