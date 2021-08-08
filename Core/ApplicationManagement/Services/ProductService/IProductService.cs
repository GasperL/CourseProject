using System.Threading.Tasks;
using Core.Common.Options;
using Core.Common.ViewModels;

namespace Core.ApplicationManagement.Services.ProductService
{
    public interface IProductService
    {
        Task Create(CreatingProductOptions options);
        
        // Task<ProductViewModel[]> GetAllAvailableProducts();
        //
        // Task<ProductViewModel[]> GetAllUnavailableProducts();

        Task<ProductViewModel[]> GetAllProducts();

        Task<ProductViewModel> GetViewModel();
    }
}