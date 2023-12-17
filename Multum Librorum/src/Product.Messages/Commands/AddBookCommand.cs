using CQRS.Core.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Messages.Commands
{
    public class AddBookCommand: Command
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
