using Marte.EventSourcing.Core.Aggregate;

namespace Marte.EventSourcing.Core.Abstract
{
    public interface IAggregateReporitory
    {
        Task StoreAsync(AggregateRoot aggregate, CancellationToken ct = default);

        Task<T> LoadAsyncVersion<T>(Guid id, int? version = null, CancellationToken ct = default) where T : AggregateRoot;

        Task<T> LoadAsync<T>(Guid id, CancellationToken ct = default) where T : AggregateRoot;
    }
}
