using dbPersistance.enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.Interfaces
{
    public interface IEntityRepository<TEntity> where TEntity : class, IPocoEntity
    {
        TEntity GetById(object id);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);

        int Count();

        int CountSelectively(Expression<Func<TEntity, bool>> filter = null);

        bool CheckIfExists(object id);

        IQueryable<TChild> GetSubType<TChild>(Expression<Func<TChild, bool>> filter = null);

        IQueryable<TChild> GetSubTypeAsNoTracking<TChild>(Expression<Func<TChild, bool>> filter = null);

        IQueryable<TEntity> GetChunksOf(int skip, int pageSize);
        IQueryable<TEntity> GetChunksOf(int skip, int pageSize, sortOrder sortOrder);
        IQueryable<TEntity> GetChunksOfWithIntOrderBy(int skip, int pageSize, sortOrder sortOrder, Expression<Func<TEntity, int>> orderBy, Expression<Func<TEntity, bool>> filter = null);
        IQueryable<TEntity> GetChunksOfWithStringOrderBy(int skip, int pageSize, sortOrder sortOrder, Expression<Func<TEntity, string>> orderBy, Expression<Func<TEntity, bool>> filter = null);
        IQueryable<TEntity> GetChunksOfWithBoolOrderBy(int skip, int pageSize, sortOrder sortOrder, Expression<Func<TEntity, bool>> orderBy, Expression<Func<TEntity, bool>> filter = null);
        IQueryable<TEntity> GetChunksOfWithDecimalOrderBy(int skip, int pageSize, sortOrder sortOrder, Expression<Func<TEntity, decimal>> orderBy, Expression<Func<TEntity, bool>> filter = null);
        IQueryable<TEntity> GetChunksOfWithDateTimeOrderBy(int skip, int pageSize, sortOrder sortOrder, Expression<Func<TEntity, DateTime>> orderBy, Expression<Func<TEntity, bool>> filter = null);

        bool Any(Expression<Func<TEntity, bool>> filter = null);

        // async functions

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<TEntity> GetByIDAsync(object id);

        IQueryable<TEntity> GetQueryable(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");


        IEnumerable<TEntity> GetAsNoTracking(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");

        IQueryable<TEntity> GetQueryableAsNoTracking(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");

        DbSet<TEntity> getDbSet();
    }
}
