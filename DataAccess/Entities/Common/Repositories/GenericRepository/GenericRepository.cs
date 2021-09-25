using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

        public async Task<TEntity> Add(TEntity entity)
        {
           var entry = await _dbSet.AddAsync(entity);
           return entry.Entity;
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

        public async Task<TResult> GetSingleWithInclude<TResult>(
            Expression<Func<TEntity, bool>> filter, 
            Expression<Func<TEntity, TResult>> selector, 
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await
                Include(includeProperties)
                    .Where(filter)
                    .Select(selector)
                    .SingleOrDefaultAsync();
        }

        public async Task<TResult[]> GetWithInclude<TResult>(
            Expression<Func<TEntity, TResult>> selector, 
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await
                Include(includeProperties)
                    .Select(selector)
                    .ToArrayAsync();
        }

        public async Task<TEntity> GetSingleOrDefault(
            Expression<Func<TEntity, bool>> single,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await
                Include(includeProperties)
                    .SingleOrDefaultAsync(single);
        }
        
        public async Task<TEntity[]> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(filter)
                .ToArrayAsync();
        }
        
        public async Task<TResult[]> GetAll<TResult>(
            Expression<Func<TEntity, bool>> filter, 
            Expression<Func<TEntity, TResult>> selector)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(filter)
                .Select(selector)
                .ToArrayAsync();
        }

        public async Task<TResult[]> GetWithInclude<TResult>(
                Expression<Func<TEntity, bool>> filter,
                Expression<Func<TEntity, TResult>> selector, 
                params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await
                Include(includeProperties)
                    .Where(filter)
                    .Select(selector)
                    .ToArrayAsync();
        }

        public async Task<TEntity> GetEntityById(Guid entityId)
        {
            return await _dbSet.FindAsync(entityId).AsTask();
        }

        public Task<TEntity[]> GetWithIncludable(
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            var query = _dbSet.AsNoTracking();
            query = include(query);
            return query.ToArrayAsync();
        }

        public async Task<TEntity> GetEntityById(string entityId)
        {
            var entity = await _dbSet.FindAsync(entityId).AsTask();

            return entity ?? throw new ArgumentNullException("entity not found");
        }

        public Task Update(TEntity item)
        {
            _dbSet.Update(item);
            
            return Task.CompletedTask;
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return 
                includeProperties
                    .Aggregate(_dbSet
                    .AsNoTracking(), (current, property) => current
                    .Include(property));
        }
    }
}