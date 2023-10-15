using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Commands
{
    public interface ICommand<R>: IRequest<R>
    {

    }

    public interface ICommand : IRequest
    {
    }
}
