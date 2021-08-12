using System.Threading.Tasks;
using DataAccess.Entities.Common.Repositories.GenericRepository;

namespace DataAccess.Entities.Common.Repositories.ProductRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product[]> GetAllAvailableProducts();

        Task<Product[]> GetAllUnavailableProducts();
    }
}