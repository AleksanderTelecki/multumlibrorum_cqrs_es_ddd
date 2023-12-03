using Employee.Messages.Enums;
using Employee.Messages.Events;
using JasperFx.Core;
using Marte.EventSourcing.Core.Aggregate;
using System.Diagnostics.Metrics;
using System.IO;

namespace Employee.Domain.Aggregates
{
    public class Employee : AggregateRoot
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public EmployeeRole Role { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RegDate { get; set; }

        public Employee()
        {
            
        }

        public Employee(string email, string password, string name, string surname, EmployeeRole role)
        {
            Id = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(string.IsNullOrWhiteSpace(email) ? nameof(email) : nameof(password));

            RaiseEvent(new EmployeeRegisteredEvent
            {
                Id = Id,
                Email = email,
                Password = password,
                Name = name,
                Surname = surname,
                Role = role,
                RegDate = DateTime.Now,
            });
        }

        public void UpdateInfo(string name, string surname)
        {
            RaiseEvent(new EmployeeNameUpdatedEvent
            {
                Id = Id,
                Name = name,
                Surname = surname
            });
        }

        public void ChangeRole(EmployeeRole newRole)
        {
            RaiseEvent(new EmployeeRoleChangedEvent
            {
                Id = Id,
                NewRole = newRole,
                OldRole = Role
            });
        }

        public void ChangePassword(string newPassword)
        {
            RaiseEvent(new EmployeePasswordChangedEvent
            {
                Id = Id,
                NewPassword = newPassword,
                OldPassword = Password
            });
        }

        public void Apply(EmployeeRegisteredEvent @event)
        {
            Id = @event.Id;
            Email = @event.Email;
            Password = @event.Password;
            Name = @event.Name;
            Surname = @event.Surname;
            Role = @event.Role;
            RegDate = @event.RegDate;

            Version++;
        }

        public void Apply(EmployeeNameUpdatedEvent @event)
        {
            Name = @event.Name;
            Surname = @event.Surname;

            Version++;
        }

        public void Apply(EmployeeRoleChangedEvent @event)
        {
            Role = @event.NewRole;

            Version++;
        }

        public void Apply(EmployeePasswordChangedEvent @event)
        {
            Password = @event.NewPassword;

            Version++;
        }

    }
}
