using CQRS.Core.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Messages.Commands
{
    public class CreateCartCommand: ICommand
    {
        public Guid ClientId { get; set; }
    }
}
