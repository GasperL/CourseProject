using System;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace ProductManagement.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task Add(TEntity entity);

        Task Delete(Guid entityId);

        Task<TEntity[]> GetAll();

        Task<TEntity> GetEntityById(Guid entityId);

        Task Update(TEntity item);
    }
}