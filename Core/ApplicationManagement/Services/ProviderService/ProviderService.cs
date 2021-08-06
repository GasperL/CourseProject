using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Options;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;

namespace Core.ApplicationManagement.Services.ProviderService
{
    public class ProviderService : IProviderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProviderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(CreatingProviderOptions options)
        {
            var guidId = Guid.NewGuid();
                
            await _unitOfWork.Providers.Add(new Provider
            {
                Id = guidId,
                Name = options.Name
            });
        }

        public async Task<ProviderViewModel[]> GetAll()
        {
            var providers =  await _unitOfWork.Providers.GetAll();

            var models = providers.Select(x => new ProviderViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToArray();

            return models;
        }
    }
}