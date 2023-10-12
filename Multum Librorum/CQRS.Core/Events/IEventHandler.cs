﻿using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Events
{
    public interface IEventHandler<T> where T : Event
    {
        Task On(T @event);
    }
}