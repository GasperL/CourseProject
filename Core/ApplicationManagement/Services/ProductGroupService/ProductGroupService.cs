using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;

namespace Core.ApplicationManagement.Services.ProductGroupService
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductGroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(CreateProductGroupViewModel options)
        {
            var id = Guid.NewGuid();
            
            await _unitOfWork.ProductGroups.Add(new ProductGroup
            {
                Id = id,
                Name = options.Name,
                Discount = options.Discount
            });
            
            await _unitOfWork.Commit();
        }

        public async Task<ProductGroupViewModel[]> GetAll()
        {
            var productGroups = await _unitOfWork.ProductGroups.GetAll();

            return productGroups.Select(x => new ProductGroupViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Discount = x.Discount,
                }).ToArray();
        }

        public async Task Remove(Guid id)
        {
            await _unitOfWork.ProductGroups.Delete(id);
            await _unitOfWork.Commit();
        }
    }
}