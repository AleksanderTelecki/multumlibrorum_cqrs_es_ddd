using Microsoft.EntityFrameworkCore;
using Client.Domain.Repository.Entity;

namespace Client.Domain
{
    public class ClientDomainDataContext: DbContext
    {
        private readonly DbContextOptions _options;
        public ClientDomainDataContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        public DbSet<ClientEntity> Users { get; set; }
    }
}
