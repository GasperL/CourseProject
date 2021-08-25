using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Entities.Common.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task Add(TEntity entity);

        Task Delete(Guid entityId);

        Task<TEntity[]> GetAll();

        Task<TResult[]> GetAll<TResult>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TResult>> selector);
        
        Task<TResult[]> GetWithInclude<TResult>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TResult>> selector,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetSingleWithFilter(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, bool>> single,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetSingle(
            Expression<Func<TEntity, bool>> single,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity[]> GetAll(Expression<Func<TEntity, bool>> filter);
        
        Task<TEntity> GetEntityById(string entityId);
        
        Task<TEntity> GetEntityById(Guid entityId);
        
        Task Update(TEntity item);
    }
}