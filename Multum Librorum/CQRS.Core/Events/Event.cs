using CQRS.Core.Messages;

namespace CQRS.Core.Events
{
    public abstract class Event: Message
    {
        protected Event() => Type = this.GetType().AssemblyQualifiedName;

        public string Type { get; set; }
    }
}
