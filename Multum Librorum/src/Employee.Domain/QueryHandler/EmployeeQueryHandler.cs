using CQRS.Core.Queries.Abstract;
using Employee.Domain.Repository;
using Employee.Messages.Models;
using Employee.Messages.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.QueryHandler
{
    public class EmployeeQueryHandler:
        IQueryHandler<GetEmployeeByEmail, EmployeeInfo>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeInfo> Handle(GetEmployeeByEmail query, CancellationToken cancellationToken)
        {
            var employeeEntity = await _employeeRepository.GetEmployeeByEmail(query.Email);

            if (employeeEntity == null)
                return null;

            return new EmployeeInfo
            {
                Id = employeeEntity.Id,
                Email = employeeEntity.Email,
                Name = employeeEntity.Name,
                Role = employeeEntity.Role,
                Password = employeeEntity.Password,
            };
        }
    }
}
