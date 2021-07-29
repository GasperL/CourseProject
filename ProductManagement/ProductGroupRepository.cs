using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Entities;

namespace ProductManagement
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly ApplicationContext _context;

        public ProductGroupRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Task<Group> Add(Group group)
        {
            throw new NotImplementedException();
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