﻿using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Messages.Events
{
    public class BookPromotedPriceChangedEvent: Event
    {
        public decimal? PromotedPrice { get; set; }
    }
}
