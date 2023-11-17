using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Messages.Events
{
    public class UserPasswordChangedEvent: Event
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
