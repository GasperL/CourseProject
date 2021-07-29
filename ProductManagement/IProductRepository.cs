using System;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace ProductManagement
{
    public interface IProductRepository
    {
        Task<Product> Add();

        Task Delete(Guid id);

        Task GetAll();

        Task GetById(Guid id);
    }
}