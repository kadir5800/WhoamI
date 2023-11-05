using  WhoamI.Core.Domain.Entities;
using  WhoamI.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WhoamI.Data.EntityFrameworkCore.Core.Repositories
{
    public interface IEfCoreRepository<TDbContext, TEntity> : IRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class, IEntity
    {
        TDbContext DbContext { get; }

        DbSet<TEntity> DbSet { get; }
    }

    public interface IEfCoreRepository<TDbContext, TEntity, TKey> : IEfCoreRepository<TDbContext, TEntity>, IRepository<TEntity, TKey>
        where TDbContext : DbContext
        where TEntity : class, IEntity<TKey>
    {

    }
}
