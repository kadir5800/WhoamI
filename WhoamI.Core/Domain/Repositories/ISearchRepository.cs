using  WhoamI.Core.Domain.Entities;
using System.Collections.Generic;

namespace  WhoamI.Core.Domain.Repositories
{
    public interface ISearchRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        TKey Save(TEntity entity);
        TEntity Get(TKey id);
        void Update(TEntity entity);
        bool Delete(TKey id);
        IEnumerable<TEntity> All();

        TKey SaveWithDeleteExisting(TEntity entity);
    }
}
