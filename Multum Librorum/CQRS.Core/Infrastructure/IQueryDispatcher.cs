using CQRS.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Infrastructure
{
    public interface IQueryDispatcher
    {
        void RegisterHandler<TQuery, QResult>(Func<TQuery, Task<QResult>> handler) where TQuery : Query<QResult>;
        Task<R> SendAsync<R>(Query<R> query);
    }
}
