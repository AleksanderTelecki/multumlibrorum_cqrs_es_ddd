using CQRS.Communication.Enums;
using CQRS.Core.Queries.Abstract;

namespace CQRS.Communication.Abstract;

public interface IRestDispatcher
{
    Task<TResult> DispatchQuery<TResult>(Query<TResult> query, EndpointEnum endpoint);
}