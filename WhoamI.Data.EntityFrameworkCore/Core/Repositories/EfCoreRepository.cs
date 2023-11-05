using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WhoamI.Core.Domain.Entities;
using WhoamI.Core.Domain.Repositories;

namespace WhoamI.Data.EntityFrameworkCore.Core.Repositories
{
    public class EfCoreRepository<TDbContext, TEntity> : RepositoryBase<TEntity>, IEfCoreRepository<TDbContext, TEntity>
        where TDbContext : DbContext
        where TEntity : class, IEntity
    {
        public TDbContext DbContext { get; }

        public DbSet<TEntity> DbSet { get; }

        public EfCoreRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public override TEntity Insert(TEntity entity, bool autoSave = false)
        {
            var savedEntity = DbSet.Add(entity).Entity;

            if (autoSave)
            {
                DbContext.SaveChanges();
            }

            return savedEntity;
        }

        public override async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var savedEntity = DbSet.Add(entity).Entity;

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }

            return savedEntity;
        }

        public override void InsertBulk(List<TEntity> entities, bool autoSave = false)
        {
            DbSet.AddRange(entities);

            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public override async Task InsertBulkAsync(List<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbSet.AddRange(entities);

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public override TEntity Update(TEntity entity, bool autoSave = false)
        {
            DbContext.Attach(entity);

            var updatedEntity = DbContext.Update(entity).Entity;

            if (autoSave)
            {
                DbContext.SaveChanges();
            }

            return updatedEntity;
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbContext.Attach(entity);

            var updatedEntity = DbContext.Update(entity).Entity;

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }

            return updatedEntity;
        }

        public override void Delete(TEntity entity, bool autoSave = false)
        {
            DbSet.Remove(entity);

            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public override async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbSet.Remove(entity);

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public override List<TEntity> GetList(bool includeDetails = false)
        {
            return includeDetails
                ? WithDetails().ToList()
                : DbSet.ToList();
        }

        public override async Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await WithDetails().ToListAsync(cancellationToken)
                : await DbSet.ToListAsync(cancellationToken);
        }

        public override long GetCount()
        {
            return DbSet.LongCount();
        }

        public override async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.LongCountAsync(cancellationToken);
        }

        protected override IQueryable<TEntity> GetQueryable()
        {
            return DbSet.AsQueryable();
        }

        public override void Delete(Expression<Func<TEntity, bool>> predicate, bool autoSave = false)
        {
            base.Delete(predicate, autoSave);

            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public override async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entities = await GetQueryable().Where(predicate).ToListAsync(cancellationToken);
            foreach (var entity in entities)
            {
                DbSet.Remove(entity);
            }

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public override IQueryable<TEntity> Queryable(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = GetQueryable();

            if (propertySelectors != null && propertySelectors.Length > 0)
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query.AsNoTracking();
        }

        public override IQueryable<TEntity> WithDetails()
        {
            return base.WithDetails();
        }

        public override IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = GetQueryable();

            if (propertySelectors != null && propertySelectors.Length > 0)
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }
    }

    public class EfCoreRepository<TDbContext, TEntity, TKey> : EfCoreRepository<TDbContext, TEntity>,
        IEfCoreRepository<TDbContext, TEntity, TKey>
        where TDbContext : DbContext
        where TEntity : class, IEntity<TKey>
    {
        public EfCoreRepository(TDbContext dbContext)
            : base(dbContext)
        {

        }

        public virtual TEntity Get(TKey id, bool includeDetails = true)
        {
            var entity = Find(id, includeDetails);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public virtual async Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, includeDetails, cancellationToken);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public virtual TEntity Find(TKey id, bool includeDetails = true)
        {
            return includeDetails
                ? WithDetails().FirstOrDefault(EntityHelper.CreateEqualityExpressionForId<TEntity, TKey>(id))
                : DbSet.Find(id);
        }

        public virtual async Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await WithDetails().FirstOrDefaultAsync(EntityHelper.CreateEqualityExpressionForId<TEntity, TKey>(id), cancellationToken)
                : await DbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual void Delete(TKey id, bool autoSave = false)
        {
            var entity = Find(id, includeDetails: false);
            if (entity == null)
            {
                return;
            }

            Delete(entity, autoSave);
        }

        public virtual async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, includeDetails: false, cancellationToken: cancellationToken);
            if (entity == null)
            {
                return;
            }

            await DeleteAsync(entity, autoSave, cancellationToken);
        }
    }
}
