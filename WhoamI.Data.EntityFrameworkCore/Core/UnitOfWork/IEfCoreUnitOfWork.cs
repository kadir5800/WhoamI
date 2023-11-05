using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using WhoamI.Core.UnitOfWork;

namespace WhoamI.Data.EntityFrameworkCore.Core.UnitOfWork
{
    public interface IEfCoreUnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        TDbContext DbContext { get; }
        IDbContextTransaction DbContextTransaction { get; }
        IsolationLevel? IsolationLevel { get; }
    }

    public interface IEfCoreUnitOfWork<TDbContext, TEntity, TKey> : IEfCoreUnitOfWork<TDbContext>, IUnitOfWork
        where TDbContext : DbContext
    {
    }
}
