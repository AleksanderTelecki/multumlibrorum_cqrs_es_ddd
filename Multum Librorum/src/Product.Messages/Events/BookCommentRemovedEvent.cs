using CQRS.Core.Events;
using Product.Messages.Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Messages.Events
{
    public class BookCommentRemovedEvent: Event
    {
        public Comment Comment { get; set; }
    }
}
