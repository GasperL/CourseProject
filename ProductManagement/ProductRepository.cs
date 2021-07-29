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

        public Task GetAllAvailableProducts()
        {
            throw new NotImplementedException();
        }

        public Task GetAllUnavailableProducts()
        {
            throw new NotImplementedException();
        }

        public Task GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task GetProductById()
        {
            throw new NotImplementedException();
        }
    }
}