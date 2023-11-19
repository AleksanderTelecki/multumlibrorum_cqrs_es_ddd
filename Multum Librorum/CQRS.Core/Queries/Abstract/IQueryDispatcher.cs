using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Queries.Abstract
{
    public interface IQueryDispatcher
    {
        Task<TResult> Dispatch<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);

    }
}
