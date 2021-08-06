using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Options;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;

namespace Core.ApplicationManagement.Services.ProductGroupService
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductGroupService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(CreatingProductGroupOptions options)
        {
            var id = Guid.NewGuid();
            
            await _unitOfWork.ProductGroups.Add(new ProductGroup
            {
                Id = id,
                Name = options.Name,
                Discount = options.Discount,
                BonusPoints = options.BonusPoints
            });
        }

        public async Task<ProductGroupViewModel[]> GetAll()
        {
            var productGroups = await _unitOfWork.ProductGroups.GetAll();

            var productGroupViewModels =
                productGroups.Select(x => new ProductGroupViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Discount = x.Discount,
                    BonusPoints = x.BonusPoints
                }).ToArray();

            return productGroupViewModels;
        }
    }
}