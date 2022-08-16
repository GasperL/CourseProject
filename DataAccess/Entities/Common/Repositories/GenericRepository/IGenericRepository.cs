#nullable enable
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Entities.Common.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity> Add(TEntity entity);

        Task Delete(Guid entityId);

        Task<TEntity> GetEntityById(string entityId);
        
        Task<TEntity> GetEntityById(Guid entityId);
        
        Task<TResult> GetSingle<TResult>(
            bool isTracking,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties);
        
        Task<TResult?> GetSingleOrDefault<TResult>(
            bool isTracking,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties);
        
        Task<TResult> GetFirst<TResult>(
            bool isTracking,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties);
        
        Task<TResult?> GetFirstOrDefault<TResult>(
            bool isTracking,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties);
        
        Task<List<TResult>> GetList<TResult>(
            bool isTracking,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties);
        
        Task Update(TEntity item);
    }
}