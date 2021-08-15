using System.Threading.Tasks;
using DataAccess.Entities.Common.Repositories.GenericRepository;

namespace DataAccess.Entities.Common.Repositories.ProductRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }
      
        public async Task<Product[]> GetAllAvailableProducts()
        {
            return await GetAll(x => x.IsAvailable == true);
        }
        
        public async Task<Product[]> GetAllUnavailableProducts()
        {
            return await GetAll(x => x.IsAvailable == false);
        }
    }
}