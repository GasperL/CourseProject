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

        public async Task<ProviderRequestViewModel[]> GetAll()
        {
            var requests = await _unitOfWork.ProviderRequest
                .GetAll();
            
            return requests.Select(x => new ProviderRequestViewModel
            {
                Id = x.Id,
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
                Id = request.Id,
                UserId = request.UserId,
                ProviderId = request.ProviderId,
                Status = request.Status,
            };
        }

        public async Task CreateRequest(CreateProviderViewModel viewModel)
        {
            var requests = await _unitOfWork.ProviderRequest
                .GetAll(x => 
                    x.UserId == viewModel.UserId, p=> 
                    p.Provider);
            
            var providerId = await CheckingRequestStatus(requests);
            
            await _unitOfWork.ProviderRequest.Add(new ProviderRequest
            {
                Id = Guid.NewGuid(),
                UserId = viewModel.UserId,
                Description = viewModel.Description,
                Name = viewModel.Name,
                Provider = new Provider
                {
                    Description = viewModel.Description,
                    Name = viewModel.Name,
                    Id = providerId,
                    IsApproved = false
                },
                Status = ProviderRequestStatusEnum.Requested,
            });

            await _unitOfWork.Commit();
        }

        public async Task ApproveProviderRequest(Guid requestId)
        {
            var request = await _unitOfWork.ProviderRequest.GetEntityById(requestId);
            var provider = await _unitOfWork.Provider.GetEntityById(request.ProviderId);

            provider.IsApproved = true;

            await ChangeStatus(request, ProviderRequestStatusEnum.Approved);
        }

        public async Task DeclineProviderRequest(Guid requestId)
        {
            var request = await _unitOfWork.ProviderRequest.GetEntityById(requestId);

            await ChangeStatus(request, ProviderRequestStatusEnum.Declined);
        }

        private async Task<Guid> CheckingRequestStatus(ProviderRequest[] requests)
        {
            var status = await GetRequestStatus(requests);

            return status switch
            {
                ProviderRequestStatusEnum.Requested => throw new Exception($"Request has already been send"),
                ProviderRequestStatusEnum.Approved => throw new Exception($"Request already has been approved"),
                ProviderRequestStatusEnum.Declined => await IfRequestStatusDecline(requests, status),
                _ => throw new InvalidOperationException()
            };
        }
        private Task<Guid> IfRequestStatusDecline(
            ProviderRequest[] requests, 
            ProviderRequestStatusEnum status)
        {
            if (status == ProviderRequestStatusEnum.Declined)
            {
                 Task.FromResult(requests
                    .Where(x => !x.Provider.IsApproved)
                    .Select(x => x.ProviderId)
                    .LastOrDefault());
            }

            return Task.FromResult(Guid.Empty);
        }
        
        private Task<ProviderRequestStatusEnum> GetRequestStatus(
            ProviderRequest[] requests)
        {
            return Task.FromResult(requests
                .Select(x => x.Status)
                .LastOrDefault());
        }

        private async Task ChangeStatus(ProviderRequest request, ProviderRequestStatusEnum status)
        {
            request.Status = status;

            await _unitOfWork.Commit();
        }
    }
}