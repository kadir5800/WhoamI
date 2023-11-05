using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using  WhoamI.Core.Domain.Entities;

namespace  WhoamI.Core.Domain.Repositories
{
    public interface IReadOnlyBasicRepository<TEntity> : IRepository
        where TEntity : class, IEntity
    {
        List<TEntity> GetList(bool includeDetails = false);

        Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default);

        long GetCount();

        Task<long> GetCountAsync(CancellationToken cancellationToken = default);
    }

    public interface IReadOnlyBasicRepository<TEntity, TKey> : IReadOnlyBasicRepository<TEntity>
        where TEntity : class, IEntity<TKey>
    {
        TEntity Get(TKey id, bool includeDetails = true);

        Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);

        TEntity Find(TKey id, bool includeDetails = true);

        Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);
    }
}
