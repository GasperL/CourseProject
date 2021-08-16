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

        Task<TEntity[]> GetAll<T>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, T>> include);

        Task<TEntity[]> GetAll(Expression<Func<TEntity, bool>> filter);
        
        Task<TEntity> GetEntityById(Guid entityId);

        Task Update(TEntity item);
    }
}