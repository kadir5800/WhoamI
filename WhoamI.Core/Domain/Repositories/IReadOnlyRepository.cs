using System.Linq.Expressions;
using  WhoamI.Core.Domain.Entities;

namespace  WhoamI.Core.Domain.Repositories
{
    public interface IReadOnlyRepository<TEntity> : IQueryable<TEntity>, IReadOnlyBasicRepository<TEntity>
         where TEntity : class, IEntity
    {
        IQueryable<TEntity> Queryable(params Expression<Func<TEntity, object>>[] propertySelectors);

        IQueryable<TEntity> WithDetails();

        IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors);
    }

    public interface IReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity>, IReadOnlyBasicRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}
