using System;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace ProductManagement
{
    public interface IProductRepository<T>
    {
        Task Add(T entity);

        Task Delete(Guid entityId);

        Task<Product[]> GetAllAvailableProducts();

        Task<Product[]> GetAllUnavailableProducts();

        Task<Product[]> GetAll();

        Task<Product> GetProductById(Guid entityId);
    }
}