using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Messages.Events
{
    public class CartItemAddedEvent: Event
    {
        public Guid ItemId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
