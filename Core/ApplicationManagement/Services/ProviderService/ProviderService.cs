using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.CreateViewModels;
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

        public async Task<ProviderRequestViewModel[]> GetAllRequests()
        {
            var requests = await _unitOfWork.ProviderRequest.GetAll();

            return requests.Select(x => new ProviderRequestViewModel
            {
                UserId = x.UserId,
                ProviderId = x.ProviderId,
                Name = x.Name,
                Description = x.Description,
                Status = x.Status
            }).ToArray();
        }

        public async Task<ProviderRequestViewModel> GetRequestModel(Guid id)
        {
            var request = await _unitOfWork.ProviderRequest.GetEntityById(id);

            return new ProviderRequestViewModel
            {
                UserId = request.UserId,
                ProviderId = request.ProviderId,
                Status = request.Status,
            };
        }

        public async Task<Guid> CreateRequest(CreateProviderViewModel viewModel)
        {
            var requests = await _unitOfWork.ProviderRequest
                .GetAll(x => x.User.Id == viewModel.UserId);

            if (requests.Select(x => x.Status).Last() != ProviderRequestStatusEnum.Approved
            || requests.Select(x => x.Status).Last() == ProviderRequestStatusEnum.Requested)
            {
                return Guid.Empty;
            }
            
            var id = Guid.NewGuid();

            await _unitOfWork.ProviderRequest.Add(new ProviderRequest
            {
                Id = id,
                UserId = viewModel.UserId,
                Description = viewModel.Description,
                Name = viewModel.Name,
                Provider = new Provider
                {
                    Description = viewModel.Description,
                    Name = viewModel.Name,
                    Id = new Guid()
                },
                Status = ProviderRequestStatusEnum.Requested,
            });

            await _unitOfWork.Commit();

            return id;
        }

        public async Task ApproveProvider(Guid requestId)
        {
            var request = await _unitOfWork.ProviderRequest.GetEntityById(requestId);

            await _unitOfWork.Provider.Add(new Provider
            {
                Id = request.ProviderId,
                Name = request.Name,
                Description = request.Description,
            });

            await ChangeStatus(request, ProviderRequestStatusEnum.Approved);
        }

        public async Task DeclineProvider(Guid requestId)
        {
            var request = await _unitOfWork.ProviderRequest.GetEntityById(requestId);
            await _unitOfWork.Provider.Delete(request.ProviderId);
            await ChangeStatus(request, ProviderRequestStatusEnum.Declined);
        }

        private async Task ChangeStatus(ProviderRequest request, ProviderRequestStatusEnum status)
        {
            request.Status = status;

            await _unitOfWork.Commit();
        }
    }
}