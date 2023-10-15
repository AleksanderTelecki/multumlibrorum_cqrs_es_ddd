using Microsoft.EntityFrameworkCore;
using Product.Domain.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain
{
    public class ProductDomainDataContext : DbContext
    {
        private readonly DbContextOptions _options;

        public ProductDomainDataContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        public DbSet<BookEntity> Books { get; set; }
        public DbSet<PromotionEntity> Promotions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BookEntity>()
                .HasMany(e => e.Promotions)
                .WithMany(e => e.PromotedBooks)
                .UsingEntity("BookPromotions",j =>
                {
                    j.Property("PromotionsId").HasColumnName("PromotionId");
                    j.Property("PromotedBooksId").HasColumnName("BookId");
                });

            base.OnModelCreating(modelBuilder);
        }

    }
}
