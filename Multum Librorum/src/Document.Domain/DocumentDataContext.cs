using Document.Domain.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Document.Domain;

public class DocumentDataContext: DbContext
{
    private readonly DbContextOptions _options;
    public DocumentDataContext(DbContextOptions options) : base(options)
    {
        _options = options;
    }
    
    public DbSet<FileEntity> Files { get; set; }
}
