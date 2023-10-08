using CQRS.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Events
{
    public abstract class Event: Message
    {
        protected Event(string type) => Type = type;

        public string Type { get; set; }
    }
}
