using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Messages.Events
{
    public class BookDetailsUpdatedEvent : Event
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string Description { get; set; }
    }
}
