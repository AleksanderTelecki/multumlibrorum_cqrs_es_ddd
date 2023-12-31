﻿using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Messages.Events
{
    public class EmployeePasswordChangedEvent : Event
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
