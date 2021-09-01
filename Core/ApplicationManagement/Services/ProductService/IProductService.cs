using System;
using System.Threading.Tasks;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;

namespace Core.ApplicationManagement.Services.ProductService
{
    public interface IProductService
    {
        Task Add(CreateProductViewModel viewModel);

        Task<ProductViewModel[]> GetAll();
        
        Task<ProductViewModel[]> GetAvailableProducts();

        Task<CreateProductViewModel> GetCreateProductViewModel(string userId);
        
        Task Deactivate(Guid productId);
        
        Task Activate(Guid productId);
        
        Task<ProductViewModel> GetProductViewModel(Guid productId);
    }
}