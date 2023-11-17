using CQRS.Core.Commands.Abstract;
using Marte.EventSourcing.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Aggregates;
using User.Messages.Commands;

namespace User.Domain.CommandHandlers
{
    public class UserCommandHandler :
        ICommandHandler<RegisterUserCommand>,
        ICommandHandler<UpdateProfileInfoCommand>,
        ICommandHandler<ChangeUserPasswordCommand>
    {

        private readonly IAggregateReporitory _aggregateReporitory;

        public UserCommandHandler(IAggregateReporitory aggregateReporitory)
        {
            _aggregateReporitory = aggregateReporitory;
        }

        public async Task Handle(RegisterUserCommand command, CancellationToken cancellation)
        {
            var user = new Aggregates.User(
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
            var user = await _aggregateReporitory.LoadAsync<Aggregates.User>(command.Id);

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
            var user = await _aggregateReporitory.LoadAsync<Aggregates.User>(command.Id);

            user.ChangePassword(command.NewPassword);

            await _aggregateReporitory.StoreAsync(user);
        }
    }
}
