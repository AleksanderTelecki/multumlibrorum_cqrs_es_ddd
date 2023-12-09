using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Messages.Events
{
    public class CartCreatedEvent: Event
    {
        public Guid ClientId { get; set; }
    }
}
