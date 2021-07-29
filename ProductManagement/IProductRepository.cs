using System.Threading.Tasks;
using DataAccess.Entities;

namespace ProductManagement
{
    public interface IProductRepository
    {
        Task<Product[]> GetAllAvailableProducts();

        Task<Product[]> GetAllUnavailableProducts();
    }
}