using System.Threading.Tasks;
using Core.Common.Options;
using Core.Common.ViewModels;
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

        public async Task Create(CreatingManufacturerOptions options)
        {
            
        }

        public Task<ManufacturerViewModel[]> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}