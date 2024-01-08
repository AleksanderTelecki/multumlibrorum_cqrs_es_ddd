using CQRS.Core.Queries.Abstract;

namespace Sales.Messages.Queries;

public class GetClientCartIdQuery: IQuery<Guid>
{
    public Guid ClientId { get; set; }
}