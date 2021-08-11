using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;
using CreateProductViewModel = Core.Common.CreateViewModels.CreateProductViewModel;

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
                Price = viewModel.Price,
                IsAvailable = false,
                ProductName = viewModel.ProductName,
                Amount = viewModel.Amount
            });
            
            await _unitOfWork.Commit();
        }

        public async Task<CreateProductViewModel> CreateProductViewModel(string userId)
        {
            var selectGroups = await GetSelectingGroup();
            var selectManufacturer = await GetSelectingManufacturer();
            var selectCategory = await GetSelectingCategory();

            var user = await _unitOfWork.Users.FindUserById(userId);

            var userRoles = await _unitOfWork.Users.GetUserRoleIds(user);
            
            return new CreateProductViewModel
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
        
        private async Task<SelectListItem[]> GetSelectingCategory()
        {
            var categories = await _unitOfWork.Categories.GetAll();

            var selectList = categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToArray();

            return selectList;
        }
        
        private async Task<SelectListItem[]> GetSelectingManufacturer()
        {
            var manufacturers = await _unitOfWork.Manufacturers.GetAll();

            var selectList = manufacturers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToArray();

            return selectList;
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