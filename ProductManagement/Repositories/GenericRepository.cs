using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Repositories
{
    public class GenericRepository<TEntity>  : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(
            DbContext context, 
            DbSet<TEntity> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await Update(entity);
        }

        public async Task Delete(Guid entityId)
        {
            var entity = await GetEntityById(entityId);
            _dbSet.Remove(entity);
        }

        public  Task<TEntity[]> GetAll()
        {
            return Task.FromResult(_dbSet.AsNoTracking().ToArray());
        }

        public async Task<TEntity> GetEntityById(Guid entityId)
        {
            var result = await _dbSet.FindAsync(entityId);
            
            return result;
        }
        
        public async Task Update(TEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}