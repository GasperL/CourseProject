using System;
using System.Threading.Tasks;
using AutoMapper;
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
            
            await AddPhoto(viewModel.Photo, id);
            await _unitOfWork.Commit();
        }

        public async Task<CreateProductViewModel> GetCreateProductViewModel(string userId)
        {
            var selectGroups = await GetSelectingGroup();
            var selectManufacturer = await GetSelectingManufacturer();
            var selectCategory = await GetSelectingCategory();

            var provider = await _unitOfWork.Provider.GetSingleWithFilter(
                x => x.ProviderRequestId == userId,
                s => s.ProviderRequest.Status == ProviderRequestStatus.Approved);

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
        
        public async Task<ProductViewModel[]> GetAvailableProducts()
        {
            var model =  _mapper.Map<ProductViewModel[]>(await _unitOfWork.Products.GetWithInclude(
                product => product.IsAvailable,
                product => product,
                p => p.ProductGroup,
                p => p.Photos));

            return model;
        }
        
        public async Task<ProductViewModel[]> GetAll() 
        {
            var model =  _mapper.Map<ProductViewModel[]>(await _unitOfWork.Products.GetWithInclude(
                product => true,
                product => product,
                p => p.ProductGroup,
                p => p.Photos,
                g => g.ProductGroup));

            return model;
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
    }
}