using CQRS.Core.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Messages.Commands
{
    public class EditCartItemCommand: ICommand
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int NewQuantity { get; set; }
    }
}
