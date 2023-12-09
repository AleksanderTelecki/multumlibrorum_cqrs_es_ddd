﻿using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Messages.Events
{
    public class CartItemRemovedEvent: Event
    {
        public Guid ProductId { get; set; }
        public Guid ItemId { get; set; }
    }
}
