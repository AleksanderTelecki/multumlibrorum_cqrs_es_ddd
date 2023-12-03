using CQRS.Core.Events;

namespace Employee.Messages.Events
{
    public class EmployeeNameUpdatedEvent : Event
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
