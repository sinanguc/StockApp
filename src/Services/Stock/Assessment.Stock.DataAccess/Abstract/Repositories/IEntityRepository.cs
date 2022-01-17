using Assessment.Stock.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Assessment.Stock.DataAccess.Abstract.Repositories
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        #region Queries

        #region Sync
        TEntity GetById(params object[] id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null);

        TEntity LastOrDefault(Expression<Func<TEntity, bool>> filter = null);


        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, bool ignoreQueryFilters = false);

        long Count(Func<TEntity, bool> filter = null);

        bool Any(Expression<Func<TEntity, bool>> condition);
        #endregion

        #region Async
        Task<TEntity> GetByIdAsync(params object[] id);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
    bool disableTracking = true, bool ignoreQueryFilters = false);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition);
        #endregion

        #endregion

        #region Commands
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        #endregion
    }
}
