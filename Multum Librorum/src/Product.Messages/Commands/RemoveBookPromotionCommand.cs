using CQRS.Core.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Messages.Commands
{
    public class RemoveBookPromotionCommand: Command
    {
        public Guid Id { get; set; }
    }
}
