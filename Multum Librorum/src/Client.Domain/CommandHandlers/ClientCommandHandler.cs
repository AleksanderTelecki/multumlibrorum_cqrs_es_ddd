using CQRS.Core.Commands.Abstract;
using CQRS.Core.Queries.Abstract;
using Marte.EventSourcing.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain.Aggregates;
using Client.Messages.Commands;
using Client.Messages.Models;
using Client.Messages.Queries;

namespace Client.Domain.CommandHandlers
{
    public class ClientCommandHandler :
        ICommandHandler<RegisterUserCommand>,
        ICommandHandler<UpdateProfileInfoCommand>,
        ICommandHandler<ChangeUserPasswordCommand>
    {

        private readonly IAggregateReporitory _aggregateReporitory;
        private readonly IQueryDispatcher _queryDispatcher;

        public ClientCommandHandler(IAggregateReporitory aggregateReporitory, IQueryDispatcher queryDispatcher)
        {
            _aggregateReporitory = aggregateReporitory;
            _queryDispatcher = queryDispatcher;
        }

        public async Task Handle(RegisterUserCommand command, CancellationToken cancellation)
        {
            var existingUser = await _queryDispatcher.Dispatch(new GetClientByEmailQuery { Email = command.Email });

            if (existingUser != null)
                throw new ArgumentOutOfRangeException("User with this email already existsin system");

            var user = new Aggregates.Client(
                command.Email,
                command.Password, 
                command.Name, 
                command.Surname, 
                command.DateOfBirth, 
                command.Street, 
                command.City, 
                command.Region, 
                command.PostalCode, 
                command.Country, 
                command.Phone);

            await _aggregateReporitory.StoreAsync(user);
        }

        public async Task Handle(UpdateProfileInfoCommand command, CancellationToken cancellation)
        {
            var user = await _aggregateReporitory.LoadAsync<Aggregates.Client>(command.Id);

            user.UpdateInfo(
                command.Name, 
                command.Surname, 
                command.DateOfBirth, 
                command.Street, 
                command.City, 
                command.Region, 
                command.PostalCode, 
                command.Country, 
                command.Phone);

            await _aggregateReporitory.StoreAsync(user);
        }

        public async Task Handle(ChangeUserPasswordCommand command, CancellationToken cancellation)
        {
            var user = await _aggregateReporitory.LoadAsync<Aggregates.Client>(command.Id);

            user.ChangePassword(command.NewPassword);

            await _aggregateReporitory.StoreAsync(user);
        }
    }
}
