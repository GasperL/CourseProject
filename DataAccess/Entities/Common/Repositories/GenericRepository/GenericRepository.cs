#nullable enable
using System;
using System.Collections.Generic;
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

        public async Task<TEntity> GetEntityById(Guid entityId)
        {
            return await _dbSet.FindAsync(entityId).AsTask();
        }

        public async Task<TEntity> GetEntityById(string entityId)
        {
            var entity = await _dbSet.FindAsync(entityId).AsTask();

            return entity ?? throw new ArgumentNullException($"entity not found");
        }

        public async Task<TResult> GetSingle<TResult>(
            bool isTracking,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(isTracking, includeProperties);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Select(selector).SingleAsync();
        }

        public async Task<TResult?> GetSingleOrDefault<TResult>(
            bool isTracking,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(isTracking, includeProperties);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Select(selector).SingleOrDefaultAsync();
        }

        public async Task<TResult> GetFirst<TResult>(
            bool isTracking,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(isTracking, includeProperties);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Select(selector).FirstAsync();
        }

        public async Task<TResult?> GetFirstOrDefault<TResult>(
            bool isTracking,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(isTracking, includeProperties);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Select(selector).FirstOrDefaultAsync();
        }

        public async Task<List<TResult>> GetList<TResult>(
            bool isTracking,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(isTracking, includeProperties);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Select(selector).ToListAsync();
        }
        
        public Task Update(TEntity item)
        {
            _dbSet.Update(item);

            return Task.CompletedTask;
        }

        private IQueryable<TEntity> Include(
            bool isTracking,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _dbSet.AsNoTracking();

            if (isTracking)
            {
                query = _dbSet.AsTracking();
            }

            query = includeProperties
                .Aggregate(query, (current, includeProperty)
                    => current.Include(includeProperty));

            return query;
        }
    }
}