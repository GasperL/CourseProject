using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Entities;

namespace ProductManagement
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Add(Product product)
        {
            if (_context.Product.All(x => x.Id != product.Id))
            {
                var result =await _context.Product.AddAsync(product);
                
                await Update(result.Entity);
            }
        }

        public async Task Delete(Guid id)
        {
            var product = await GetProductById(id);
            
            _context.Product.Remove(product);
        }

        public Task<Product[]> GetAllAvailableProducts()
        {
            var products = _context.Product.Local
                .Where(x => x.Accessibility == true)
                .ToArray();
            
            return Task.FromResult(products);
        }

        public Task<Product[]> GetAllUnavailableProducts()
        {
            var products = _context.Product.Local
                .Where(x => x.Accessibility == false)
                .ToArray();
            
            return Task.FromResult(products);
        }

        public Task<Product[]> GetAllProducts()
        {
            var products = _context.Product.Local
                .ToArray();
            
            return Task.FromResult(products);
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _context.Product.FindAsync(id);
        }

        private async Task Update(Product product)
        {
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}