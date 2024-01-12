using Client.Messages.Models;
using CQRS.Core.Queries.Abstract;

namespace Client.Messages.Queries;

public class GetClientsQuery: IQuery<List<ClientDetails>>
{
    
}