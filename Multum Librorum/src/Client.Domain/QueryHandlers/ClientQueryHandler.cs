﻿using CQRS.Core.Queries.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Domain.Repository;
using Client.Domain.Repository.Entity;
using Client.Messages.Models;
using Client.Messages.Queries;
using Marten.Linq.QueryHandlers;

namespace Client.Domain.QueryHandlers
{
    public class ClientQueryHandler : 
        IQueryHandler<GetClientByEmailQuery, ClientInfo>,
        IQueryHandler<GetClientDetailsQuery, ClientDetails>,
        IQueryHandler<GetClientsQuery, List<ClientDetails>>
    {

        private readonly IClientRepository _userRepository;

        public ClientQueryHandler(IClientRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ClientInfo> Handle(GetClientByEmailQuery query, CancellationToken cancellation)
        {
            var userEntity = await _userRepository.GetClientByEmail(query.Email);

            if (userEntity == null)
                return null;
            
            if (userEntity.IsBlocked == true)
                return null;

            return new ClientInfo { 
                Id = userEntity.Id,
                Email = userEntity.Email,
                Name = userEntity.Name,
                Password = userEntity.Password,
            };
        }

        public async Task<ClientDetails> Handle(GetClientDetailsQuery query, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetClient(query.ClientId);
            
            if (userEntity == null)
                return null;
            
            return new ClientDetails() { 
                Id = userEntity.Id,
                Email = userEntity.Email,
                Surname = userEntity.Surname,
                Name = userEntity.Name,
                Phone = userEntity.Phone,
                PostalCode = userEntity.PostalCode,
                DateOfBirth = userEntity.DateOfBirth,
                Street = userEntity.Street,
                City = userEntity.City,
                Region = userEntity.Region,
                Country = userEntity.Country,
                IsBlocked = userEntity.IsBlocked
            };
            
        }

        public async Task<List<ClientDetails>> Handle(GetClientsQuery query, CancellationToken cancellationToken)
        {
            var userEntitys = await _userRepository.GetClients();
            
            List<ClientDetails> clientDetailsList = new();
            
            foreach (var userEntity in userEntitys)
            {
                clientDetailsList.Add(
                    new ClientDetails() { 
                        Id = userEntity.Id,
                        Email = userEntity.Email,
                        Surname = userEntity.Surname,
                        Name = userEntity.Name,
                        Phone = userEntity.Phone,
                        PostalCode = userEntity.PostalCode,
                        DateOfBirth = userEntity.DateOfBirth,
                        Street = userEntity.Street,
                        City = userEntity.City,
                        Region = userEntity.Region,
                        Country = userEntity.Country,
                        IsBlocked = userEntity.IsBlocked
                    }
                );
            }
            
            return clientDetailsList;
        }
    }
}
