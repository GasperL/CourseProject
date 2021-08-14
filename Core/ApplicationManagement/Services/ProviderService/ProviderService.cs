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

        public async Task<ProviderRequestViewModel[]> GetAllActiveRequests()
        {
            var requests = await _unitOfWork.ProviderRequest
                .GetAll(x => x.Status == ProviderRequestStatusEnum.Requested);

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
                .GetAll(x => x.UserId == viewModel.UserId);
            
            var requestId = Guid.NewGuid();
            var providerId = await CheckingRequestsStatus(requests);
            
            await _unitOfWork.ProviderRequest.Add(new ProviderRequest
            {
                Id = requestId,
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

            return requestId;
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
        
        private async Task<Guid> CheckingRequestsStatus(ProviderRequest[] requests)
        {
            await AssertRequestStatus(requests);
            return IfRequestIsDecline(requests);
        }

        private Guid IfRequestIsDecline(ProviderRequest[] requests)
        {
            var isDecline = CheckRequestByStatus(requests, ProviderRequestStatusEnum.Declined);

            if (isDecline)
            {
                return requests
                    .Where(x => x.ProviderId != Guid.Empty)
                    .Select(x => x.ProviderId)
                    .LastOrDefault();
            }

            return Guid.Empty;
        }

        private Task AssertRequestStatus(ProviderRequest[] requests)
        {
            var isRequested = CheckRequestByStatus(requests, ProviderRequestStatusEnum.Requested);

            // todo custom exception
            if (isRequested)
            {
                throw new Exception($"Request has already been send"); 
            }

            var isApproved = CheckRequestByStatus(requests, ProviderRequestStatusEnum.Approved);

            if (isApproved)
            {
                throw new Exception($"Request already has been approved");
            }
            
            return Task.CompletedTask;
        }
        
        private bool CheckRequestByStatus(ProviderRequest[] requests, ProviderRequestStatusEnum status)
        {
            return requests
                .Select(x => x.Status)
                .LastOrDefault() == status;
        }

        private async Task ChangeStatus(ProviderRequest request, ProviderRequestStatusEnum status)
        {
            request.Status = status;

            await _unitOfWork.Commit();
        }
    }
}