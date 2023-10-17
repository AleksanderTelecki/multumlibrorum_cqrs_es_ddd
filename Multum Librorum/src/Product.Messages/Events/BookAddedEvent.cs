using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Messages.Events
{
    public  class BookAddedEvent: Event
    {
        // Book properties
        public string Title { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public DateTime RegDate { get; set; }
        public string Description { get; set; }


        // Sales properties
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
