using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain.Repository.Entity;

namespace Client.Domain.Repository
{
    public interface IClientRepository
    {
        public Task CreateClient(ClientEntity user);
        public Task<ClientEntity> GetClient(Guid id);
        public Task<ClientEntity> GetClientByEmail(string email);
        public Task UpdateClient(ClientEntity user);
    }

    public class ClientRepository : IClientRepository
    {
        private readonly ClientDomainDataContext _userDomainDataContext;

        public ClientRepository(ClientDomainDataContext userDomainDataContext)
        {
            _userDomainDataContext = userDomainDataContext;
        }

        public async Task CreateClient(ClientEntity user)
        {
            _userDomainDataContext.Users.Add(user);
            await _userDomainDataContext.SaveChangesAsync();
        }

        public async Task<ClientEntity> GetClient(Guid id)
        {
            return await _userDomainDataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ClientEntity> GetClientByEmail(string email)
        {
            return await _userDomainDataContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task UpdateClient(ClientEntity user)
        {
            _userDomainDataContext.Users.Update(user);
            await _userDomainDataContext.SaveChangesAsync();
        }
    }
}
