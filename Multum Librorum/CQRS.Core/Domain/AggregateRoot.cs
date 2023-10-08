using CQRS.Core.Events;
using Newtonsoft.Json;

namespace CQRS.Core.Domain
{
    public abstract class AggregateRoot
    {
        // For indexing our event streams
        public Guid Id { get; protected set; }

        // For protecting the state, i.e. conflict prevention
        // The setter is only public for setting up test conditions
        public long Version { get; set; }

        // JsonIgnore - for making sure that it won't be stored in inline projection
        [JsonIgnore] private readonly List<Event> _uncommittedEvents = new List<Event>();

        // Get the deltas, i.e. events that make up the state, not yet persisted
        public IEnumerable<Event> GetUncommittedEvents()
        {
            return _uncommittedEvents;
        }

        // Mark the deltas as persisted.
        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        protected void AddUncommittedEvent(Event @event)
        {
            // add the event to the uncommitted list
            _uncommittedEvents.Add(@event);
        }

        private void ApplyChanges(Event @event)
        {
            var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });
            if (method == null)
                throw new ArgumentNullException(nameof(method), $"The apply method was not found in the aggregate root for {@event.GetType().Name}");

            method.Invoke(this, new object[] { @event });

            AddUncommittedEvent(@event);
        }

        protected void RaiseEvent(Event @event)
        {
            ApplyChanges(@event);
        }
    }
}
