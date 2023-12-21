using CQRS.Communication.Enums;
using CQRS.Core.Commands.Abstract;
using CQRS.Core.Queries.Abstract;

namespace CQRS.Communication.Abstract;

public interface IRestDispatcher
{
    Task<TResult> DispatchQuery<TResult>(IQuery<TResult> query, EndpointEnum endpoint);
    Task DispatchCommand<TCommand>(TCommand command, EndpointEnum endpoint) where TCommand : ICommand;
}