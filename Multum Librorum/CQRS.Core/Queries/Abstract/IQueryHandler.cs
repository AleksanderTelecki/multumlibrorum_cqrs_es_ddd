using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Queries.Abstract
{
    public interface IQueryHandler<in TQuery, TQueryResult> where TQuery : IQuery
    {
        Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);
    }
}
