using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Entities.Common.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity> Add(TEntity entity);

        Task Delete(Guid entityId);

        Task<TEntity[]> GetAll();

        Task<TResult[]> GetAll<TResult>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TResult>> selector);
        
        Task<TResult[]> GetWithInclude<TResult>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TResult>> selector,
            params Expression<Func<TEntity, object>>[] includeProperties);
        
        Task<TResult> GetSingleWithInclude<TResult>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TResult>> selector,
            params Expression<Func<TEntity, object>>[] includeProperties);
        
        Task<TResult[]> GetWithInclude<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetSingleOrDefault(
            Expression<Func<TEntity, bool>> single,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity[]> GetAll(Expression<Func<TEntity, bool>> filter);
        
        Task<TEntity> GetEntityById(string entityId);
        
        Task<TEntity> GetEntityById(Guid entityId);
        
        Task<TEntity[]> GetWithIncludable(
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);
        
        Task Update(TEntity item);
    }
}