using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Commands.Abstract
{
    public interface ICommandDispatcher
    {
        Task Dispatch<TCommand>(TCommand command, CancellationToken cancellation = default) where TCommand: ICommand;
    }
}
