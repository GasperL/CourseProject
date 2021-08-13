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
            
            await _unitOfWork.Manufacturers.Add(new Manufacturer
            {
                Id = Guid.NewGuid(),
                Name = viewModel.Name
            });
        }

        public async Task<ManufacturerViewModel[]> GetAll()
        {
            var manufacturers = await _unitOfWork.Manufacturers.GetAll();

            return manufacturers.Select(x => new ManufacturerViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToArray();
        }
    }
}