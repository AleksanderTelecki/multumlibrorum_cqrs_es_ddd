namespace CQRS.Core.Commands.Abstract
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command, CancellationToken cancellation);
    }
}
