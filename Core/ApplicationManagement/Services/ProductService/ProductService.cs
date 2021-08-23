using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.Utils;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
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
                Amount = viewModel.Amount,
            });

            await AddPhoto(viewModel.Photo, id);

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
            var photos = await _unitOfWork.Files.GetAll();

            return photos.Zip(products, 
                (photo, product) => new ProductViewModel
                {
                    Id = product.Id,
                    Category = product.Category,
                    ProductGroup = product.ProductGroup,
                    Manufacturer = product.Manufacturer,
                    Provider = product.Provider,
                    IsAvailable = product.IsAvailable,
                    ProductName = product.ProductName,
                    Amount = product.Amount,
                    Price = product.Price,
                    PhotoBase64 = FileUtils.GetPhotoBase64(photo.Image)
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

        private async Task AddPhoto(IFormFile file, Guid productId)
        {
            var id = Guid.NewGuid();
            var fileBytes = await FileUtils.GetFileBytes(file);

            await _unitOfWork.Files.Add(new ProductPhoto
            {
                Id = id,
                Image = fileBytes,
                ProductId = productId
            });
        }
    }
}