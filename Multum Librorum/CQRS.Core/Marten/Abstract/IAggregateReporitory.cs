using CQRS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Marten.Abstract
{
    public interface IAggregateReporitory
    {
        Task StoreAsync(AggregateRoot aggregate, CancellationToken ct = default);

        Task<T> LoadAsyncVersion<T>(Guid id, int? version = null, CancellationToken ct = default) where T : AggregateRoot;

        Task<T> LoadAsync<T>(Guid id, CancellationToken ct = default) where T : AggregateRoot;
    }
}
