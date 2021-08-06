using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Options;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;

namespace Core.ApplicationManagement.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        
        public async Task<ProductViewModel[]> GetAll()
        {
            var products = await _unitOfWork.Products.GetAll();

            return products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Category = x.Category,
                ProductGroup = x.ProductGroup,
                Provider = x.Provider,
                Manufacturer = x.Manufacturer,
                IsAvailable = x.IsAvailable,
                ProductName = x.ProductName,
                Amount = x.Amount,
                Price = x.Price
            }).ToArray();
        }
        
        public async Task<ProductViewModel[]> GetAllAvailableProducts()
        {
            var products = await _unitOfWork.Products.GetAllAvailableProducts();

            return products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Category = x.Category,
                ProductGroup = x.ProductGroup,
                Provider = x.Provider,
                Manufacturer = x.Manufacturer,
                IsAvailable = x.IsAvailable,
                ProductName = x.ProductName,
                Amount = x.Amount,
                Price = x.Price
            }).ToArray();
        }

        public async Task<ProductViewModel[]> GetAllUnavailableProducts()
        {
            var products = await _unitOfWork.Products.GetAllUnavailableProducts();

            var productModels = products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Category = x.Category,
                ProductGroup = x.ProductGroup,
                Provider = x.Provider,
                Manufacturer = x.Manufacturer,
                IsAvailable = x.IsAvailable,
                ProductName = x.ProductName,
                Amount = x.Amount,
                Price = x.Price
            }).ToArray();

            return productModels;
        }
    }
}