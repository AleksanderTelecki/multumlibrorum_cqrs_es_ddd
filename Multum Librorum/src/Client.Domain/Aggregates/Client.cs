using Marte.EventSourcing.Core.Aggregate;
using Client.Messages.Events;

namespace Client.Domain.Aggregates
{
    public class Client: AggregateRoot
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }
        public string Phone { get; private set; }
        public DateTime RegDate { get; private set; }
        
        public bool IsBlocked { get; private set; } 

        public Client()
        {
            
        }

        public Client(string email, string password, string name, string surname, DateTime dateOfBirth, string street, string city, string region, string postalCode, string country, string phone)
        {
            Id = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(string.IsNullOrWhiteSpace(email) ? nameof(email) : nameof(password));

            RaiseEvent(new ClientRegisteredEvent
            {
                Id = Id,
                Email = email,
                Password = password,
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                Street = street,
                City = city,
                Region = region,
                PostalCode = postalCode,
                Country = country,
                Phone = phone,
                RegDate = DateTime.Now,
            });
        }

        public void UpdateInfo(string name, string surname, DateTime dateOfBirth, string street, string city, string region, string postalCode, string country, string phone)
        {
            RaiseEvent(new ClientProfileInfoUpdatedEvent
            {
                Id = Id,
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                Street = street,
                City = city,
                Region = region,
                PostalCode = postalCode,
                Country = country,
                Phone = phone,
            });
        }

        public void Block()
        {
            RaiseEvent(new ClientBlockedEvent()
            {
                Id = Id
            });
        }
        
        public void UnBlock()
        {
            RaiseEvent(new ClientUnBlockedEvent()
            {
                Id = Id
            });
        }

        public void ChangePassword(string newPassword)
        {
            RaiseEvent(new ClientPasswordChangedEvent
            {
                Id = Id,
                NewPassword = newPassword,
                OldPassword = Password
            });
        }

        public void Apply(ClientPasswordChangedEvent @event)
        {
            Password = @event.NewPassword;

            Version++;
        }

        public void Apply(ClientRegisteredEvent @event)
        {
            Id = @event.Id;
            Email = @event.Email;
            Password = @event.Password;
            Name = @event.Name;
            Surname = @event.Surname;
            DateOfBirth = @event.DateOfBirth;
            Street = @event.Street;
            City = @event.City;
            Region = @event.Region;
            PostalCode = @event.PostalCode;
            Country = @event.Country;
            Phone = @event.Phone;
            RegDate = @event.RegDate;

            Version++;
        }

        public void Apply(ClientProfileInfoUpdatedEvent @event)
        {
            Name = @event.Name;
            Surname = @event.Surname;
            DateOfBirth = @event.DateOfBirth;
            Street = @event.Street;
            City = @event.City;
            Region = @event.Region;
            PostalCode = @event.PostalCode;
            Country = @event.Country;
            Phone = @event.Phone;

            Version++;
        }
        
        public void Apply(ClientBlockedEvent @event)
        {
            IsBlocked = true;

            Version++;
        }
        
        public void Apply(ClientUnBlockedEvent @event)
        {
            IsBlocked = false;

            Version++;
        }
    }
}
