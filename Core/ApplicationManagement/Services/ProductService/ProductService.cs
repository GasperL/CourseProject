using System;
using System.Linq;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.CategoryService;
using Core.ApplicationManagement.Services.ManufacturerService;
using Core.ApplicationManagement.Services.ProductGroupService;
using Core.Common.Options;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Entities.Common.Repositories.UserRepository;
using DataAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.ApplicationManagement.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductGroupService _group;
        private readonly ICategoryService _category;
        private readonly IManufacturerService _manufacturer;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(
            IUnitOfWork unitOfWork, 
            ICategoryService category, 
            IProductGroupService group, 
            IManufacturerService manufacturer)
        {
            _unitOfWork = unitOfWork;
            _category = category;
            _group = group;
            _manufacturer = manufacturer;
        }

        public async Task Create(CreatingProductOptions options)
        {
            var id = Guid.NewGuid(); 
                
            await _unitOfWork.Products.Add(new Product
            {
                Id = id,
                Category = options.Category,
                CategoryId = options.Category.Id,
                IsAvailable = false,
                Manufacturer = options.Manufacturer,
                ManufacturerId = options.Manufacturer.Id,
                Price = options.Price,
                ProductGroup = options.Group,
                ProductGroupId = options.Group.Id,
                ProductName = options.ProductName,
                Amount = options.Amount
            });
        }

        public async Task<ProductViewModel> GetViewModel()
        {
            var selectGroups = await GetSelectingGroup();
            var selectManufacturer = await GetSelectingManufacturer();
            var selectCategory = await GetSelectingCategory();

            return new ProductViewModel
            {
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

        // public async Task<ProductViewModel[]> GetAllAvailableProducts()
        // {
        //     var products = await _unitOfWork.Products.GetAllAvailableProducts();
        //     var selectCategory = await GetSelectingCategory();
        //     var selectGroup = await GetSelectingGroup();
        //     
        //     return products.Select(x => new ProductViewModel
        //     {
        //         Id = x.Id,
        //         SelectCategory = selectCategory,
        //         SelectProductGroups = selectGroup,
        //         Provider = x.Provider,
        //         ManufacturerId = x.ManufacturerId,
        //         IsAvailable = x.IsAvailable,
        //         ProductName = x.ProductName,
        //         Amount = x.Amount,
        //         Price = x.Price
        //     }).ToArray();
        // }
        //
        // public async Task<ProductViewModel[]> GetAllUnavailableProducts()
        // {
        //     var products = await _unitOfWork.Products.GetAllUnavailableProducts();
        //     var selectCategory = await GetSelectingCategory();
        //     var selectGroup = await GetSelectingGroup();
        //     var selectManufacturers = await GetSelectingManufacturer();
        //
        //
        //     var productModels = products.Select(x => new ProductViewModel
        //     {
        //         Id = x.Id,
        //         SelectCategory = selectCategory,
        //         SelectProductGroups = selectGroup,
        //         Provider = x.Provider,
        //         SelectManufacturer = selectManufacturers,
        //         ManufacturerId = x.ManufacturerId,
        //         IsAvailable = x.IsAvailable,
        //         ProductName = x.ProductName,
        //         Amount = x.Amount,
        //         Price = x.Price
        //     }).ToArray();
        //
        //     return productModels;
        // }
        
        private async Task<SelectList> GetSelectingCategory()
        {
            var categories = await _unitOfWork.Categories.GetAll();

            var categoriesName = categories
                .Select(x => x.Name);

            var categoriesValue = categories
                .Select(x => x.Id);

            var selectList = new SelectList (categoriesName, categoriesValue);

            return selectList;
        }
        
        private async Task<SelectList> GetSelectingManufacturer()
        {
            var manufacturers = await _unitOfWork.Manufacturers.GetAll();

            var manufacturerNames = manufacturers
                .Select(x => x.Name);

            var manufacturerValues = manufacturers
                .Select(x => x.Id);

            var selectList = new SelectList (manufacturerNames, manufacturerValues);

            return selectList;
        }

        private async Task<SelectList> GetSelectingGroup()
        {
            var productGroups = await _unitOfWork.ProductGroups.GetAll();

            var groupNames = productGroups
                .Select(x => x.Name);

            var groupsValues = productGroups
                .Select(x => x.Id);

            var selectList = new SelectList (groupNames, groupsValues);

            return selectList;
        }
    }
}