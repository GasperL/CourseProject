using System;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;

namespace Core.ApplicationManagement.Services.ProductGroupService
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductGroupService(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(CreateProductGroupViewModel options)
        {
            await _unitOfWork.ProductGroups.Add(new ProductGroup
            {
                Id = Guid.NewGuid(),
                Name = options.Name,
                Discount = options.Discount
            });
            
            await _unitOfWork.Commit();
        }

        public async Task<ProductGroupViewModel[]> GetAll()
        {
            var productGroups = await _unitOfWork.ProductGroups.GetList(
                    isTracking: false, 
                    selector: s => s);
            
            return _mapper.Map<ProductGroupViewModel[]>(productGroups);
        }
        
        public async Task Remove(Guid id)
        {
            await _unitOfWork.ProductGroups.Delete(id);
            await _unitOfWork.Commit();
        }

        public async Task<ProductGroupViewModel> GetProductGroupViewModel(Guid id)
        {
            var group = await _unitOfWork.ProductGroups.GetEntityById(id);
                
            return _mapper.Map<ProductGroupViewModel>(group);
        }
      
        public async Task Edit(ProductGroupViewModel model)
        {
            var group =  await _unitOfWork.ProductGroups.GetEntityById(model.Id);

            group.Discount = model.Discount;
            group.Name = model.Name;

            await _unitOfWork.ProductGroups.Update(group);
            await _unitOfWork.Commit();
        }
      
    }
}