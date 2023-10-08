using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Queries
{
    public interface IQueryHandler<T, R> where T: Query<R>
    {
        Task<R> HandleAsync(T query);
    }
}
