using Employee.Domain.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain
{
    public class EmployeeDataContext: DbContext
    {
        private readonly DbContextOptions _options;
        public EmployeeDataContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        public DbSet<EmployeeEntity> Employees { get; set; }
    }
}
