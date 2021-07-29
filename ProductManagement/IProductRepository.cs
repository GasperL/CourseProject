using System;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace ProductManagement
{
    public interface IProductRepository
    {
        Task Add(Product product);

        Task Delete(Guid id);

        Task<Product[]> GetAllAvailableProducts();

        Task<Product[]> GetAllUnavailableProducts();

        Task<Product[]> GetAllProducts();

        Task<Product> GetProductById(Guid id);
    }
}