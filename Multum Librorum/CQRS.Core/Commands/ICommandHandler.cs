namespace CQRS.Core.Commands
{
    public interface ICommandHandler<T> where T : Command
    {
        Task HandleAsync(T command);
    }
}
