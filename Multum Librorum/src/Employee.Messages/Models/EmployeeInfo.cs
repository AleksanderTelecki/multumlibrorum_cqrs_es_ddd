using Employee.Messages.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Messages.Models
{
    public class EmployeeInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public EmployeeRole Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
