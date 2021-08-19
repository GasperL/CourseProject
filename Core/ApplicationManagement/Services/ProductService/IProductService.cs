using System.Threading.Tasks;
using Core.Common.ViewModels;
using CreateProductViewModel = Core.Common.CreateViewModels.CreateProductViewModel;

namespace Core.ApplicationManagement.Services.ProductService
{
    public interface IProductService
    {
        Task Add(CreateProductViewModel viewModel);
        
        Task<ProductViewModel[]> GetAllProducts();

        Task<CreateProductViewModel> GetCreateProductViewModel(string userId);
    }
}