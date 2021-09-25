using System;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;

namespace Core.ApplicationManagement.Services.ManufacturerService
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManufacturerService(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(ManufacturerViewModel viewModel)
        {
            await _unitOfWork.Manufacturers.Add(new Manufacturer
            {
                Id = Guid.NewGuid(),
                Name = viewModel.Name
            });

            await _unitOfWork.Commit();
        }

        public async Task<ManufacturerViewModel[]> GetAll()
        {
            var manufacturers = await _unitOfWork.Manufacturers.GetAll();
            return _mapper.Map<ManufacturerViewModel[]>(manufacturers);
        }

        public async Task Remove(Guid categoryId)
        {
            await _unitOfWork.Manufacturers.Delete(categoryId);
            await _unitOfWork.Commit();
        }

        public async Task Edit(ManufacturerViewModel model)
        {
            var manufacturer =  await _unitOfWork.Manufacturers.GetEntityById(model.Id);

            manufacturer.Name = model.Name;

            await _unitOfWork.Manufacturers.Update(manufacturer);
            
            await _unitOfWork.Commit();
        }
        
        public async Task<ManufacturerViewModel> GetManufacturerViewModel(Guid manufacturerId)
        {
            var manufacturer = await _unitOfWork.Manufacturers.GetEntityById(manufacturerId);
            
            return _mapper.Map<ManufacturerViewModel>(manufacturer);
        }
    }
}