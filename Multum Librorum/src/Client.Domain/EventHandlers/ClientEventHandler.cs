using CQRS.Core.Events.Abstract;
using Client.Domain.Repository;
using Client.Domain.Repository.Entity;
using Client.Messages.Events;

namespace Client.Domain.EventHandlers
{
    public class ClientEventHandler :
        IEventHandler<ClientRegisteredEvent>,
        IEventHandler<ClientProfileInfoUpdatedEvent>,
        IEventHandler<ClientPasswordChangedEvent>
    {

        private readonly IClientRepository _userRepository;

        public ClientEventHandler(IClientRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(ClientRegisteredEvent @event, CancellationToken cancellation)
        {
            var userEntity = new ClientEntity
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

            await _userRepository.CreateClient(userEntity);
        }

        public async Task Handle(ClientProfileInfoUpdatedEvent @event, CancellationToken cancellation)
        {
            var userEntity = await _userRepository.GetClient(@event.Id);

            userEntity.Name = @event.Name;
            userEntity.Surname = @event.Surname;
            userEntity.Street = @event.Street;
            userEntity.City = @event.City;
            userEntity.Region = @event.Region;
            userEntity.Country = @event.Country;
            userEntity.DateOfBirth = @event.DateOfBirth;
            userEntity.Phone = @event.Phone;

            await _userRepository.UpdateClient(userEntity);
        }

        public async Task Handle(ClientPasswordChangedEvent @event, CancellationToken cancellation)
        {
            var userEntity = await _userRepository.GetClient(@event.Id);
        
            userEntity.Password = @event.NewPassword;

            await _userRepository.UpdateClient(userEntity);
        }
    }
}
