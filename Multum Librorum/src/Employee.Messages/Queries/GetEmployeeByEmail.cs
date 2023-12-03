using CQRS.Core.Queries.Abstract;
using Employee.Messages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Messages.Queries
{
    public class GetEmployeeByEmail: IQuery<EmployeeInfo>
    {
        public string Email { get; set; }
    }
}
