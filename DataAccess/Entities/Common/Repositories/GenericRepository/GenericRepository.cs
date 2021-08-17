using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities.Common.Repositories.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity

    {
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(Guid entityId)
        {
            var entity = await GetEntityById(entityId);
            _dbSet.Remove(entity);
        }

        public async Task<TEntity[]> GetAll()
        {
            return await _dbSet
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<TEntity[]> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(filter)
                .ToArrayAsync();
        }
        
        public async Task<TEntity[]> GetAll<T>(
            Expression<Func<TEntity, bool>> filter, 
            Expression<Func<TEntity, T>> include)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(include)
                .Where(filter)
                .ToArrayAsync();
        }

        public async Task<TEntity> GetEntityById(Guid entityId)
        {
            return await _dbSet.FindAsync(entityId).AsTask();
        }
        
        public async Task<TEntity> GetEntityById(string entityId)
        {
            return await _dbSet.FindAsync(entityId).AsTask();
        }

        public Task Update(TEntity item)
        {
            _dbSet.Update(item);
            return Task.CompletedTask;
        }
    }
}