using System.ComponentModel.DataAnnotations.Schema;

namespace Document.Domain.Repository.Entities;

[Table("Files", Schema = "Document")]
public class FileEntity
{
    public Guid Id { get; set; }
    public Guid SourceId { get; set; }
    public string Path { get; set; }
    public DateTime RegDate { get; set; }

    public FileEntity(Guid sourceId, string path, DateTime regDate)
    {
        Id = Guid.NewGuid();
        SourceId = sourceId;
        Path = path;
        RegDate = regDate;
    }

    public FileEntity()
    {
        
    }
}