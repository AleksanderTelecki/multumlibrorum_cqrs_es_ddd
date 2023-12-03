using CQRS.Core.Events;
using Employee.Messages.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Messages.Events
{
    public class EmployeeRoleChangedEvent : Event
    {
        public EmployeeRole OldRole { get; set; }
        public EmployeeRole NewRole { get; set; }
    }
}
