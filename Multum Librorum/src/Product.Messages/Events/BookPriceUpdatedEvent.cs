using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Messages.Events
{
    public class BookPriceUpdatedEvent: Event
    {
        public decimal Price { get; set; }
    }
}
