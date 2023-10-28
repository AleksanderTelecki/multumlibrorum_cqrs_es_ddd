using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Events.Abstract
{
    public interface IEventDispatcher
    {
        Task Dispatch(object @event, CancellationToken cancellation = default);
    }
}
