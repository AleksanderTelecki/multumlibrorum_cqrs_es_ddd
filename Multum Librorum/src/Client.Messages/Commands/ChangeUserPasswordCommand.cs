using CQRS.Core.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Messages.Commands
{
    public class ChangeUserPasswordCommand: ICommand
    {
        public Guid Id { get; set; }
        public string NewPassword { get; set; }
    }
}
