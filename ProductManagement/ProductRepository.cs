using System;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace ProductManagement
{
    public class ProductRepository : IProductRepository
    {
        public Task<Product> Add()
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
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