using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace  WhoamI.Core.UnitOfWork
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public abstract void Begin();
        public abstract Task BeginAsync(CancellationToken cancellationToken = default);
        public abstract void Commit();
        public abstract Task CommitAsync(CancellationToken cancellationToken = default);
        public abstract void Dispose();
        public abstract void Rollback();
        public abstract Task RollbackAsync(CancellationToken cancellationToken = default);
        public abstract void SaveChanges();
        public abstract Task SaveChangesAsync(CancellationToken cancellationToken = default);
        public abstract void SetIsolationLevel(IsolationLevel isolationLevel);
    }
}
