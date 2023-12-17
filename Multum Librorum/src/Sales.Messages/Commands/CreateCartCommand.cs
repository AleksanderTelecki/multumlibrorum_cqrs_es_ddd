using CQRS.Core.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Messages.Commands
{
    public class CreateCartCommand: Command
    {
        public Guid ClientId { get; set; }
    }
}
