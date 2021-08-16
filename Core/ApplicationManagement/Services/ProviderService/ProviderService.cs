#nullable enable
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
            
            // x => x.Status == ProviderRequestStatus.Requested
            
            return requests.Select(x => new ProviderRequestViewModel
            {
                Id = x.Id,
                UserId = x.UserId,
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
                Status = request.Status,
            };
        }

        public async Task CreateRequest(CreateProviderRequestViewModel requestViewModel)
        {
            var request = await FindProviderRequestByUser(requestViewModel);

            if (request == null)
            {
                await AddProviderRequest(requestViewModel);
            }
            
            await CheckStatusRequest(request);

            await UpdateProviderRequest(requestViewModel, request.Id);
        }

        private async Task CheckStatusRequest(ProviderRequest request)
        {
            switch (request.Status)
            {
                case ProviderRequestStatus.Requested or ProviderRequestStatus.Approved:
                    throw new Exception("Request already has been created or approved");
                case ProviderRequestStatus.Declined:
                    await ChangeStatus(request, ProviderRequestStatus.Requested);
                    break;
            }
        }

        public async Task ApproveProviderRequest(Guid requestId)
        {
            var request = await _unitOfWork.ProviderRequest.GetEntityById(requestId);

            await AssertRequestStatus(request);
            
            await _unitOfWork.Provider.Add(new Provider
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ProviderRequestId = request.Id
            });
            
            await ChangeStatus(request, ProviderRequestStatus.Approved);
        }

        public async Task DeclineProviderRequest(Guid requestId)
        {
            var request = await _unitOfWork.ProviderRequest.GetEntityById(requestId);

            await AssertRequestStatus(request);

            await ChangeStatus(request, ProviderRequestStatus.Declined);
        }

        private async Task ChangeStatus(ProviderRequest request, ProviderRequestStatus status)
        {
            request.Status = status;

            await _unitOfWork.Commit();
        }
        
        private static Task AssertRequestStatus(ProviderRequest request)
        {
            if (request.Status is ProviderRequestStatus.Approved or ProviderRequestStatus.Declined)
            {
                throw new Exception("Request status already was approved or declined");
            }
            
            return Task.CompletedTask;
        }
        
        private async Task<ProviderRequest?> FindProviderRequestByUser(CreateProviderRequestViewModel requestViewModel)
        {
            var requests = await _unitOfWork.ProviderRequest.GetAll();

            var request = requests.SingleOrDefault(x => x.UserId == requestViewModel.UserId);
            
            return request;
        }
        
        private async Task AddProviderRequest(CreateProviderRequestViewModel requestViewModel)
        {
            var id = Guid.NewGuid();
            
            await _unitOfWork.ProviderRequest.Add(new ProviderRequest
            {
                Id = id,
                UserId = requestViewModel.UserId,
                Description = requestViewModel.Description,
                Name = requestViewModel.Name,
                Status = ProviderRequestStatus.Requested
            });

            await _unitOfWork.Commit();
        }

        private async Task UpdateProviderRequest(CreateProviderRequestViewModel requestViewModel, Guid id)
        {
            await _unitOfWork.ProviderRequest.Update(new ProviderRequest
            {
                Id = id,
                UserId = requestViewModel.UserId,
                Description = requestViewModel.Description,
                Name = requestViewModel.Name,
                Status = ProviderRequestStatus.Requested
            });
            
            await _unitOfWork.Commit();
        }
    }
}