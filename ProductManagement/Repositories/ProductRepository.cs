using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DbContext _context;
        
        private readonly DbSet<Product> _dbSet;
        
        public ProductRepository(
            DbContext context, 
            DbSet<Product> dbSet) : base(context, dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }
        
        public Task<Product[]> GetAllAvailableProducts()
        {
            var products = _context
                .Entry(_dbSet)
                .Entity 
                .Where(x => x.Accessibility == true)
                .ToArray();
            
            return Task.FromResult(products);
        }

        public Task<Product[]> GetAllUnavailableProducts()
        {
            var products = _context
                .Entry(_dbSet)
                .Entity.Local 
                .Where(x => x.Accessibility == false)
                .ToArray();
            
            return Task.FromResult(products);
        }
    }
}