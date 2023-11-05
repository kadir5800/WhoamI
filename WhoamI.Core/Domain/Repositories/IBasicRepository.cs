using  WhoamI.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace  WhoamI.Core.Domain.Repositories
{
    public interface IBasicRepository<TEntity> : IReadOnlyBasicRepository<TEntity>
        where TEntity : class, IEntity
    {
        TEntity Insert(TEntity entity, bool autoSave = false);

        Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);        
        void InsertBulk(List<TEntity> entities, bool autoSave = false);

        Task InsertBulkAsync(List<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);

        TEntity Update(TEntity entity, bool autoSave = false);

        Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        void Delete(TEntity entity, bool autoSave = false);

        Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
    }

    public interface IBasicRepository<TEntity, TKey> : IBasicRepository<TEntity>, IReadOnlyBasicRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        void Delete(TKey id, bool autoSave = false);

        Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default);
    }
}
