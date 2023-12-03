using CQRS.Core.Commands.Abstract;
using CQRS.Core.Queries.Abstract;
using Employee.Messages.Commands;
using Employee.Messages.Queries;
using JasperFx.Core;
using Marte.EventSourcing.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.CommandHandlers
{
    public class EmployeeCommandHandler:
        ICommandHandler<RegisterEmployeeCommand>,
        ICommandHandler<ChangeEmployeePasswordCommand>,
        ICommandHandler<ChangeEmployeeRoleCommand>,
        ICommandHandler<UpdateEmployeeNameCommand>
    {

        private readonly IAggregateReporitory _aggregateReporitory;
        private readonly IQueryDispatcher _queryDispatcher;

        public EmployeeCommandHandler(IAggregateReporitory aggregateReporitory, IQueryDispatcher queryDispatcher)
        {
            _aggregateReporitory = aggregateReporitory;
            _queryDispatcher = queryDispatcher;
        }

        public async Task Handle(RegisterEmployeeCommand command, CancellationToken cancellation)
        {
            var existingEmployee = await _queryDispatcher.Dispatch(new GetEmployeeByEmail { Email = command.Email });

            if (existingEmployee != null)
                throw new ArgumentOutOfRangeException("Employee with this email already existsing system");

            var employee = new Aggregates.Employee(
                command.Email,
                command.Password,
                command.Name,
                command.Surname,
                command.Role);

            await _aggregateReporitory.StoreAsync(employee);
        }

        public async Task Handle(ChangeEmployeePasswordCommand command, CancellationToken cancellation)
        {
            var employee = await _aggregateReporitory.LoadAsync<Aggregates.Employee>(command.Id);

            employee.ChangePassword(command.NewPassword);

            await _aggregateReporitory.StoreAsync(employee);
        }

        public async Task Handle(ChangeEmployeeRoleCommand command, CancellationToken cancellation)
        {
            var employee = await _aggregateReporitory.LoadAsync<Aggregates.Employee>(command.Id);

            employee.ChangeRole(command.NewRole);

            await _aggregateReporitory.StoreAsync(employee);
        }

        public async Task Handle(UpdateEmployeeNameCommand command, CancellationToken cancellation)
        {
            var employee = await _aggregateReporitory.LoadAsync<Aggregates.Employee>(command.Id);

            employee.UpdateInfo(command.Name, command.Surname);

            await _aggregateReporitory.StoreAsync(employee);
        }
    }
}
