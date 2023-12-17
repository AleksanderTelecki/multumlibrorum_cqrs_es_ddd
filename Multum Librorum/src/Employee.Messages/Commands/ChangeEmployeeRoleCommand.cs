using CQRS.Core.Commands.Abstract;
using Employee.Messages.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Messages.Commands
{
    public class ChangeEmployeeRoleCommand: Command
    {
        public Guid Id { get; set; }
        public EmployeeRole NewRole { get; set; }
    }
}
