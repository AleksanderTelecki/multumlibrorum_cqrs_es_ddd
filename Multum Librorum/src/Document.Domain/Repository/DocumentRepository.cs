using Document.Domain.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Document.Domain.Repository;


public interface IDocumentRepository
{
    public Task CreateFile(FileEntity fileEntity);
    public Task<FileEntity> GetFileBySource(Guid sourceId);
    public Task<FileEntity> GetFileFileId(Guid fileId);
    
}

public class DocumentRepository: IDocumentRepository
{
    private readonly DocumentDataContext _documentDataContext;

    public DocumentRepository(DocumentDataContext documentDataContext)
    {
        _documentDataContext = documentDataContext;
    }


    public async Task CreateFile(FileEntity fileEntity)
    {
        _documentDataContext.Files.Add(fileEntity);
        await _documentDataContext.SaveChangesAsync();
    }

    public async Task<FileEntity> GetFileBySource(Guid sourceId)
    {
        return await _documentDataContext.Files.FirstOrDefaultAsync(x => x.SourceId == sourceId);
    }

    public async Task<FileEntity> GetFileFileId(Guid fileId)
    {
        return await _documentDataContext.Files.SingleAsync(x => x.Id == fileId);
    }
}