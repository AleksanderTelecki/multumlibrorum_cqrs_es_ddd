using CQRS.Core.Events;
using Employee.Messages.Enums;

namespace Employee.Messages.Events
{
    public class EmployeeRegisteredEvent: Event
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public EmployeeRole Role { get; set; }
        public DateTime RegDate { get; set; }
    }
}
