using Microsoft.EntityFrameworkCore;
using User.Domain.Repository.Entity;

namespace User.Domain
{
    public class UserDomainDataContext: DbContext
    {
        private readonly DbContextOptions _options;
        public UserDomainDataContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
