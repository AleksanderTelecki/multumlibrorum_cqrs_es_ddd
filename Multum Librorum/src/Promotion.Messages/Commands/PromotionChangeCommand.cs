using CQRS.Core.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Messages.Commands
{
    public class PromotionChangeCommand: ICommand
    {
        public decimal PromotionInPercentage { get; set; }
        public List<Guid> ProductIds { get; set; }
        public DateTime EndDate { get; set; }
    }
}
