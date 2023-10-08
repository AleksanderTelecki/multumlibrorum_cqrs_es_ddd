using CQRS.Core.Commands;
using CQRS.Core.Infrastructure;

namespace CQRS.Core.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly Dictionary<Type, Func<Command, Task>> _handlers = new();

        public void RegisterHandler<T>(Func<T, Task> handler) where T : Command
        {
            if (_handlers.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException("You cannot register the same command handler twice!");
            }

            _handlers.Add(typeof(T), x => handler((T)x));
        }

        public async Task SendAsync(Command command)
        {
            if (_handlers.TryGetValue(command.GetType(), out var handler))
            {
                await handler(command);
            }
            else
            {
                throw new ArgumentNullException("No command handler was register");
            }
        }
    }
}
