using CQRS.Core.Events.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Repository;
using User.Domain.Repository.Entity;
using User.Messages.Events;

namespace User.Domain.EventHandlers
{
    public class UserEventHandler :
        IEventHandler<UserRegisteredEvent>,
        IEventHandler<UserProfileInfoUpdatedEvent>,
        IEventHandler<UserPasswordChangedEvent>
    {

        private readonly IUserRepository _userRepository;

        public UserEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UserRegisteredEvent @event, CancellationToken cancellation)
        {
            var userEntity = new UserEntity
            {
                Id = @event.Id,
                Email = @event.Email,
                Password = @event.Password,
                Name = @event.Name,
                Surname = @event.Surname,
                DateOfBirth = @event.DateOfBirth,
                Street = @event.Street,
                City = @event.City,
                Region = @event.Region,
                PostalCode = @event.PostalCode,
                Country = @event.Country,
                RegDate = @event.RegDate,
                Phone = @event.Phone
            };

            await _userRepository.CreateUser(userEntity);
        }

        public async Task Handle(UserProfileInfoUpdatedEvent @event, CancellationToken cancellation)
        {
            var userEntity = await _userRepository.GetUser(@event.Id);

            userEntity.Name = @event.Name;
            userEntity.Surname = @event.Surname;
            userEntity.Street = @event.Street;
            userEntity.City = @event.City;
            userEntity.Region = @event.Region;
            userEntity.Country = @event.Country;
            userEntity.DateOfBirth = @event.DateOfBirth;
            userEntity.Phone = @event.Phone;

            await _userRepository.UpdateUser(userEntity);
        }

        public async Task Handle(UserPasswordChangedEvent @event, CancellationToken cancellation)
        {
            var userEntity = await _userRepository.GetUser(@event.Id);

            userEntity.Password = @event.NewPassword;

            await _userRepository.UpdateUser(userEntity);
        }
    }
}
