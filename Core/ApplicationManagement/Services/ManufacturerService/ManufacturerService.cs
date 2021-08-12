using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;

namespace Core.ApplicationManagement.Services.ManufacturerService
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManufacturerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(CreateManufacturerViewModel viewModel)
        {
            var id = Guid.NewGuid();
            
            await _unitOfWork.Manufacturers.Add(new Manufacturer
            {
                Id = id,
                Name = viewModel.Name
            });
        }

        public async Task<ManufacturerViewModel[]> GetAll()
        {
            var manufacturers = await _unitOfWork.Manufacturers.GetAll();

            var manufacturerViewModels = manufacturers.Select(x => new ManufacturerViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToArray();

            return manufacturerViewModels;
        }
    }
}