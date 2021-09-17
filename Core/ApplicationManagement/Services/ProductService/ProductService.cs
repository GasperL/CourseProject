using System;
using System.Threading.Tasks;
using AutoMapper;
using Core.ApplicationManagement.Exceptions;
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
        private readonly IMapper _mapper;
        
        public ProductService(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(CreateProductViewModel viewModel)
        {
            var id = Guid.NewGuid();

            var product = _mapper.Map<Product>(viewModel);
            product.Id = id;
            
            await _unitOfWork.Products.Add(product);

            foreach (var photo in viewModel.Photos)
            {
                await AddPhoto(photo, id);
            }

            await _unitOfWork.Commit();
        }

        public async Task<CreateProductViewModel> GetCreateProductViewModel(string userId)
        {
            var selectGroups = await GetSelectingGroup();
            var selectManufacturer = await GetSelectingManufacturer();
            var selectCategory = await GetSelectingCategory();

            var provider = await _unitOfWork.Providers.GetSingleOrDefault(x =>
                x.ProviderRequestId == userId && x.ProviderRequest.Status == ProviderRequestStatus.Approved);

            AssertionsUtils.AssertIsNotNull(provider, "Поставщик не найден");

            return new CreateProductViewModel
            {
                ProviderId = provider.Id,
                SelectCategory = selectCategory,
                SelectManufacturer = selectManufacturer,
                SelectProductGroups = selectGroups
            };
        }

        public async Task Deactivate(Guid productId)
        {
            var product = await _unitOfWork.Products.GetEntityById(productId);
            
            AssertProductAvailability(product, false);
            
            product.IsAvailable = false;
            
            await _unitOfWork.Commit();
        }
        
        public async Task Activate(Guid productId)
        {
            var product = await _unitOfWork.Products.GetEntityById(productId);

            AssertProductAvailability(product, true);
            AssertProductAmount(product);

            product.IsAvailable = true;
            
            await _unitOfWork.Commit();
        }

        public async Task<ProductViewModel> GetProductViewModel(Guid productId)
        {
            var products = await _unitOfWork.Products.GetSingleOrDefault(
                product => product.Id == productId,
                p => p.ProductGroup,
                p => p.Photos,
                g => g.ProductGroup,
                m => m.Manufacturer,
                pr => pr.Provider,
                c => c.Category);
            
            return  _mapper.Map<ProductViewModel>(products);
        }

        public async Task<ProductViewModel[]> GetAvailableProducts()
        {
            var product = await _unitOfWork.Products.GetWithInclude(
                p => p.IsAvailable,
                p => p,
                p => p.ProductGroup,
                p => p.Photos);

            return  _mapper.Map<ProductViewModel[]>(product);
        }
        
        public async Task<ProductViewModel[]> GetAll()
        {
            var products = await _unitOfWork.Products.GetWithInclude(
                product => product,
                p => p.ProductGroup,
                p => p.Photos,
                g => g.ProductGroup);
            
            return _mapper.Map<ProductViewModel[]>(products);
        }
        
        private async Task<SelectListItem[]> GetSelectingCategory()
        {
            var categories = await _unitOfWork.Categories.GetAll();

            return _mapper.Map<SelectListItem[]>(categories);
        }

        private async Task<SelectListItem[]> GetSelectingManufacturer()
        {
            var manufacturers = await _unitOfWork.Manufacturers.GetAll();
            
            return _mapper.Map<SelectListItem[]>(manufacturers);
        }

        private async Task<SelectListItem[]> GetSelectingGroup()
        {
            var groups = await _unitOfWork.ProductGroups.GetAll();
            return _mapper.Map<SelectListItem[]>(groups);
        }

        private async Task AddPhoto(IFormFile file, Guid productId)
        {
            var id = Guid.NewGuid();
            var fileBytes = FileUtils.GetFileBytes(file);

            await _unitOfWork.ProductPhotos.Add(new ProductPhoto
            {
                Id = id,
                Image = fileBytes,
                ProductId = productId
            });
        }

        private static void AssertProductAmount(Product product)
        {
            if (product.Amount == 0)
            {
                ThrowProductActivateException(product, "Продукт недоступен потому что его количество 0");
            }
        }

        private static void AssertProductAvailability(Product product, bool shouldProductBeAvailable)
        {
            var textError = shouldProductBeAvailable == true ? "доступен" : "недоступен";
            
            if (product.IsAvailable == shouldProductBeAvailable)
            {
                ThrowProductActivateException(product, $"Товар уже {textError} для покупки.");
            }
        }
        
        private static void ThrowProductActivateException(Product product, string text)
        {
            throw new ProductActivateException(text)
            {
                Amount = product.Amount,
                IsAvailable = product.IsAvailable,
                ProductId = product.Id
            };
        }
    }
}