using CQRS.Core.Queries.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Repository;
using User.Domain.Repository.Entity;
using User.Messages.Models;
using User.Messages.Queries;

namespace User.Domain.QueryHandlers
{
    public class UserQueryHandler
        : IQueryHandler<GetUserByEmailQuery, UserInfo>
    {

        private readonly IUserRepository _userRepository;

        public UserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserInfo> Handle(GetUserByEmailQuery query, CancellationToken cancellation)
        {
            var userEntity = await _userRepository.GetUserByEmail(query.Email);

            if (userEntity == null)
                return null;

            return new UserInfo { 
                Id = userEntity.Id,
                Email = userEntity.Email,
                Name = userEntity.Name,
                Password = userEntity.Password,
            };
        }
    }
}
