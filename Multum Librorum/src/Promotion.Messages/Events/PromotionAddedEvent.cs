using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Messages.Events
{
    public class PromotionAddedEvent: Event
    {
        public string Description { get; set; }
        public decimal PromotionInPercentage { get; set; }
        public List<Guid> Products { get; set; }
        public DateTime Regdate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
