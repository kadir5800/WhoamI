using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using WhoamI.Core.UnitOfWork;

namespace WhoamI.Data.EntityFrameworkCore.Core.UnitOfWork
{
    public class EfCoreUnitOfWork<TDbContext> : UnitOfWorkBase, IEfCoreUnitOfWork<TDbContext>
        where TDbContext : DbContext
    {
        public TDbContext DbContext { get; private set; }
        public IDbContextTransaction DbContextTransaction { get; private set; }
        public IsolationLevel? IsolationLevel { get; private set; }

        public EfCoreUnitOfWork(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public EfCoreUnitOfWork(TDbContext dbContext, IsolationLevel isolationLevel)
        {
            DbContext = dbContext;
            IsolationLevel = isolationLevel;
        }

        public override void Begin()
        {
            if (DbContextTransaction == null)
            {
                if (IsolationLevel.HasValue)
                    DbContextTransaction = DbContext.Database.BeginTransaction(IsolationLevel.GetValueOrDefault());
                else
                    DbContextTransaction = DbContext.Database.BeginTransaction();
            }
        }

        public override Task BeginAsync(CancellationToken cancellationToken = default)
        {
            Begin();
            return Task.CompletedTask;
        }

        public override void Commit()
        {
            DbContextTransaction.Commit();
        }

        public override Task CommitAsync(CancellationToken cancellationToken = default)
        {
            Commit();
            return Task.CompletedTask;
        }

        public override void Rollback()
        {
            DbContextTransaction.Rollback();
        }

        public override Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            Rollback();
            return Task.CompletedTask;
        }

        public override void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public override Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return DbContext.SaveChangesAsync();
        }

        public override void SetIsolationLevel(IsolationLevel isolationLevel)
        {
            IsolationLevel = isolationLevel;
        }

        public override void Dispose()
        {
            if (DbContextTransaction != null)
                DbContextTransaction.Dispose();

            DbContextTransaction = null;
        }
    }
}
