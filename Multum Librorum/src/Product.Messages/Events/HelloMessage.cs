using CQRS.Core.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Messages.Events
{
    public class HelloMessage : Event, INotification
    {
        public string Name { get; set; }
    }
}
