using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Entities;
using System.Linq;

namespace ProductManagement
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly ApplicationContext _context;

        public ProductGroupRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Add(ProductGroup group)
        {
            if (_context.ProductGroup.All(x => x.Id != group.Id))
            {
                await _context.AddAsync(group);
                
            }

        }

        public Task Delete(Group id)
        {
            throw new NotImplementedException();
        }

        public Task GetAll()
        {
            throw new NotImplementedException();
        }

        public Task GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}