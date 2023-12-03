using CQRS.Core.Events.Abstract;
using Employee.Domain.Repository;
using Employee.Domain.Repository.Entity;
using Employee.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.EventHandlers
{
    public class EmployeeEventHandlers: 
        IEventHandler<EmployeeRegisteredEvent>,
        IEventHandler<EmployeeNameUpdatedEvent>,
        IEventHandler<EmployeePasswordChangedEvent>,
        IEventHandler<EmployeeRoleChangedEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeEventHandlers(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(EmployeeRegisteredEvent @event, CancellationToken cancellation)
        {
            var employeeEntity = new EmployeeEntity
            {
                Id = @event.Id,
                Email = @event.Email,
                Password = @event.Password,
                Name = @event.Name,
                Surname = @event.Surname,
                Role = @event.Role,
                RegDate = @event.RegDate,
            };

            await _employeeRepository.CreateEmployee(employeeEntity);
        }

        public async Task Handle(EmployeeNameUpdatedEvent @event, CancellationToken cancellation)
        {
            var employeeEntity = await _employeeRepository.GetEmployee(@event.Id);
            employeeEntity.Name = @event.Name;
            employeeEntity.Surname = @event.Surname;

            await _employeeRepository.UpdateEmployee(employeeEntity);
        }

        public async Task Handle(EmployeePasswordChangedEvent @event, CancellationToken cancellation)
        {
            var employeeEntity = await _employeeRepository.GetEmployee(@event.Id);
            employeeEntity.Password = @event.NewPassword;

            await _employeeRepository.UpdateEmployee(employeeEntity);
        }

        public async Task Handle(EmployeeRoleChangedEvent @event, CancellationToken cancellation)
        {
            var employeeEntity = await _employeeRepository.GetEmployee(@event.Id);
            employeeEntity.Role = @event.NewRole;

            await _employeeRepository.UpdateEmployee(employeeEntity);
        }
    }
}
