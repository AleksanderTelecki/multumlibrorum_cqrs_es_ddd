﻿using CQRS.Core.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Employee.Messages.Commands
{
    public class ChangeEmployeePasswordCommand: ICommand
    {
        public Guid Id { get; set; }
        public string NewPassword { get;set; }

    }
}
