using  WhoamI.Core.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace  WhoamI.Core.Domain.Repositories
{
    public interface IRepository
    {
    }

    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>, IBasicRepository<TEntity> where TEntity : class, IEntity
    {
        void Delete(Expression<Func<TEntity, bool>> predicate, bool autoSave = false);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default);
    }

    public interface IRepository<TEntity, TKey> : IRepository<TEntity>, IReadOnlyRepository<TEntity, TKey>, IBasicRepository<TEntity, TKey>  where TEntity : class, IEntity<TKey>
    {
    }
}
