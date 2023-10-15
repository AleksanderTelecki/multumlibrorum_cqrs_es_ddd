using CQRS.Core.Messages;
using MediatR;

namespace CQRS.Core.Events
{
    public abstract class Event: Message, INotification
    {
        protected Event() => Type = this.GetType().AssemblyQualifiedName;

        public string Type { get; set; }
    }
}
