using Assessment.Stock.DataAccess.Abstract.Repositories;
using Assessment.Stock.DataAccess.Concrete.EntityFramework.Contexts;
using Assessment.Stock.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Assessment.Stock.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {

        private readonly StockContext _stockContext;
        private readonly DbSet<TEntity> _entities;

        public EfEntityRepositoryBase(StockContext stockContext)
        {
            _stockContext = stockContext;
            _entities = _stockContext.Set<TEntity>();
        }

        #region Queries

        #region Sync
        public virtual TEntity GetById(params object[] id)
        {
            return _entities.Find(id);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null)
        {
            return _entities.AsNoTracking().FirstOrDefault(filter);
        }


        public virtual TEntity LastOrDefault(Expression<Func<TEntity, bool>> filter = null)
        {
            return _entities.AsNoTracking().LastOrDefault(filter);
        }


        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
    bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = _entities;

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return orderBy != null ? orderBy(query) : query;
        }

        public virtual long Count(Func<TEntity, bool> filter = null)
        {
            return _entities.Count(filter);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> condition)
        {
            return _entities.Any(condition);
        }
        #endregion

        #region Async
        public async virtual Task<TEntity> GetByIdAsync(params object[] id)
        {
            return await _entities.FindAsync(id).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _entities.AsNoTracking().FirstOrDefaultAsync(filter).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _entities.AsNoTracking().LastOrDefaultAsync(filter).ConfigureAwait(false);
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = _entities;

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return await (orderBy != null ? orderBy(query) : query).ToListAsync();
        }

        public async virtual Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _entities.CountAsync(filter).ConfigureAwait(false);
        }


        public async virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _entities.AnyAsync(condition).ConfigureAwait(false);
        }
        #endregion

        #endregion

        #region Commands
        public virtual TEntity Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity is ISoftDeleteEntity softDeleteEntity)
                softDeleteEntity.Deleted = false;

            var addedEntity = _entities.Add(entity);
            _stockContext.Entry(entity).State = EntityState.Added;
            return addedEntity.Entity;
        }
        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _stockContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public virtual void Delete(TEntity entity)
        {
            switch (entity)
            {
                case null:
                    throw new ArgumentNullException(nameof(entity));
                case ISoftDeleteEntity softDeleteEntity:
                    softDeleteEntity.Deleted = true;
                    _entities.Attach(entity).State = EntityState.Modified;
                    break;
                default:
                    _entities.Remove(entity);
                    break;
            }
        }
        #endregion

    }
}
