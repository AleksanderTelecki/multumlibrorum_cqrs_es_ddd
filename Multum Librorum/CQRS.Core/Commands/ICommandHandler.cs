using MediatR;

namespace CQRS.Core.Commands
{
    public interface ICommandHandler<T, R>: IRequestHandler<T,R> where T : IRequest<R>
    {

    }

    public interface ICommandHandler<T> : IRequestHandler<T> where T: IRequest
    {

    }
}
