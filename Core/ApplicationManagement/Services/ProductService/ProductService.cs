using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.ApplicationManagement.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(CreateProductViewModel viewModel)
        {
            var id = Guid.NewGuid();
            
            await _unitOfWork.Products.Add(new Product
            {
                Id = id,
                CategoryId = viewModel.CategoryId,
                ProductGroupId = viewModel.ProductGroupId,
                ManufacturerId = viewModel.ManufacturerId,
                ProviderId = viewModel.ProviderId,
                Price = viewModel.Price,
                IsAvailable = false,
                ProductName = viewModel.ProductName,
                Amount = viewModel.Amount
            });
            
            await _unitOfWork.Commit();
        }

        public async Task<CreateProductViewModel> GetCreateProductViewModel(string userId)
        {
            var selectGroups = await GetSelectingGroup();
            var selectManufacturer = await GetSelectingManufacturer();
            var selectCategory = await GetSelectingCategory();

            var providers = await _unitOfWork.Provider.GetAll(
                x => x.ProviderRequestId == userId, 
                i => i.ProviderRequest);

            var provider = providers.SingleOrDefault
                (x => x.ProviderRequest.Status == ProviderRequestStatus.Approved);
            
            if (provider == null)
            {
                throw new Exception("Provider not found");
            }
            
            return new CreateProductViewModel
            {
                ProviderId = provider.Id,
                SelectCategory = selectCategory,
                SelectManufacturer = selectManufacturer,
                SelectProductGroups = selectGroups
            };
        }
        
        public async Task<ProductViewModel[]> GetAllProducts()
        {
            var products = await _unitOfWork.Products.GetAll();
            
            return products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Category = x.Category,
                ProductGroup = x.ProductGroup,
                Manufacturer = x.Manufacturer,
                ManufacturerId = x.ManufacturerId,
                Provider = x.Provider,
                IsAvailable = x.IsAvailable,
                ProductName = x.ProductName,
                Amount = x.Amount,
                Price = x.Price
            }).ToArray();
        }

        private async Task<SelectListItem[]> GetSelectingCategory()
        {
            var categories = await _unitOfWork.Categories.GetAll();

            return categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToArray();
        }
        
        private async Task<SelectListItem[]> GetSelectingManufacturer()
        {
            var manufacturers = await _unitOfWork.Manufacturers.GetAll();

            return manufacturers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToArray();
        }

        private async Task<SelectListItem[]> GetSelectingGroup()
        {
            var groups = await _unitOfWork.ProductGroups.GetAll();

            var selectList = groups.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToArray();

            return selectList;
        }
    }
}