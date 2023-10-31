using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Messages.Events
{
    public class PromotionEndedEvent: Event
    {
        public List<Guid> Products { get; set; }
    }
}
