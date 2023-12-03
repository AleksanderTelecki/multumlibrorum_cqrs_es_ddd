using Employee.Domain.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Employee.Domain.Repository
{
    public interface IEmployeeRepository
    {
        public Task CreateEmployee(EmployeeEntity employee);
        public Task<EmployeeEntity> GetEmployee(Guid id);
        public Task<EmployeeEntity> GetEmployeeByEmail(string email);
        public Task UpdateEmployee(EmployeeEntity employee);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDataContext _employeeDomainDataContext;

        public EmployeeRepository(EmployeeDataContext employeeDomainDataContext)
        {
            _employeeDomainDataContext = employeeDomainDataContext;
        }

        public async Task CreateEmployee(EmployeeEntity employee)
        {
            _employeeDomainDataContext.Employees.Add(employee);
            await _employeeDomainDataContext.SaveChangesAsync();
        }

        public async Task<EmployeeEntity> GetEmployee(Guid id)
        {
            return await _employeeDomainDataContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EmployeeEntity> GetEmployeeByEmail(string email)
        {
            return await _employeeDomainDataContext.Employees.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task UpdateEmployee(EmployeeEntity employee)
        {
            _employeeDomainDataContext.Employees.Update(employee);
            await _employeeDomainDataContext.SaveChangesAsync();
        }
    }
}
