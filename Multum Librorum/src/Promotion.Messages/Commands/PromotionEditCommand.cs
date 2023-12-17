using CQRS.Core.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Messages.Commands
{
    public class PromotionEditCommand: Command
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal PromotionInPercentage { get; set; }
        public List<Guid> Products { get; set; }
        public DateTime EndDate { get; set; }
    }
}
