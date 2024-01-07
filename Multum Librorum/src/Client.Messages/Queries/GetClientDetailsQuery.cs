using Client.Messages.Models;
using CQRS.Core.Queries.Abstract;

namespace Client.Messages.Queries;

public class GetClientDetailsQuery: IQuery<ClientDetails>
{
    public Guid ClientId { get; set; }
}