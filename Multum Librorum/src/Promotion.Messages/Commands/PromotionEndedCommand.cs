using CQRS.Core.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Messages.Commands
{
    public class PromotionEndedCommand: ICommand
    {
        public Guid Id { get; set; }
    }
}
