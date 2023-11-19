using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Repository.Entity;

namespace User.Domain.Repository
{
    public interface IUserRepository
    {
        public Task CreateUser(UserEntity user);
        public Task<UserEntity> GetUser(Guid id);
        public Task<UserEntity> GetUserByEmail(string email);
        public Task UpdateUser(UserEntity user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserDomainDataContext _userDomainDataContext;

        public UserRepository(UserDomainDataContext userDomainDataContext)
        {
            _userDomainDataContext = userDomainDataContext;
        }

        public async Task CreateUser(UserEntity user)
        {
            _userDomainDataContext.Users.Add(user);
            await _userDomainDataContext.SaveChangesAsync();
        }

        public async Task<UserEntity> GetUser(Guid id)
        {
            return await _userDomainDataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserEntity> GetUserByEmail(string email)
        {
            return await _userDomainDataContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task UpdateUser(UserEntity user)
        {
            _userDomainDataContext.Users.Update(user);
            await _userDomainDataContext.SaveChangesAsync();
        }
    }
}
