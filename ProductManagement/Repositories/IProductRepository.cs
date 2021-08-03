using System.Threading.Tasks;
using DataAccess.Entities;

namespace ProductManagement.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product[]> GetAllAvailableProducts();

        Task<Product[]> GetAllUnavailableProducts();
    }
}