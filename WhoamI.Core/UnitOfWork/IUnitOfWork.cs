using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace  WhoamI.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();

        Task BeginAsync(CancellationToken cancellationToken = default);

        void Commit();

        Task CommitAsync(CancellationToken cancellationToken = default);

        void Rollback();

        Task RollbackAsync(CancellationToken cancellationToken = default);

        void SaveChanges();

        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        void SetIsolationLevel(IsolationLevel isolationLevel);
    }
}
