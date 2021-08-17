#nullable enable
using System;
using System.Linq;
using System.Threading.Tasks;
using Core.ApplicationManagement.Exceptions;
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
                .GetAll(x => x.Status == ProviderRequestStatus.Requested);
            
            return requests.Select(x => new ProviderRequestViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Status = x.Status
            }).ToArray();
        }

        public async Task CreateRequest(CreateProviderRequestViewModel requestViewModel)
        {
            var request = await FindProviderRequestByUser(requestViewModel);

            if (request == null)
            {
                await AddProviderRequest(requestViewModel);
                
                return;
            }
            
            await CheckStatusRequest(request);

            await UpdateProviderRequest(requestViewModel, request.Id);
        }

        private async Task CheckStatusRequest(ProviderRequest request)
        {
            switch (request.Status)
            {
                case ProviderRequestStatus.Requested or ProviderRequestStatus.Approved:
                    throw new ProviderRequestException($"Request status already {request.Status}", request.Status);
                case ProviderRequestStatus.Declined:
                    await ChangeStatus(request, ProviderRequestStatus.Requested);
                    break;
            }
        }

        public async Task ApproveProviderRequest(string requestId)
        {
            var request = await _unitOfWork.ProviderRequest.GetEntityById(requestId);

            await AssertRequestStatus(request);
            
            await _unitOfWork.Provider.Add(new Provider
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
            });
            
            await ChangeStatus(request, ProviderRequestStatus.Approved);
        }

        public async Task DeclineProviderRequest(string requestId)
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
                throw new ProviderRequestException($"Request status already {request.Status}", request.Status);
            }
            
            return Task.CompletedTask;
        }
        
        private async Task<ProviderRequest?> FindProviderRequestByUser(CreateProviderRequestViewModel requestViewModel)
        {
            var requests = await _unitOfWork.ProviderRequest.GetAll();

            var request = requests.SingleOrDefault(x => requestViewModel.UserId == x.Id);
            
            return request;
        }
        
        private async Task AddProviderRequest(CreateProviderRequestViewModel requestViewModel)
        {
            await _unitOfWork.ProviderRequest.Add(new ProviderRequest
            {
                Id = requestViewModel.UserId,
                Description = requestViewModel.Description,
                Name = requestViewModel.Name,
                Status = ProviderRequestStatus.Requested
            }); 

            await _unitOfWork.Commit();
        }

        private async Task UpdateProviderRequest(CreateProviderRequestViewModel requestViewModel, string id)
        {
            await _unitOfWork.ProviderRequest.Update(new ProviderRequest
            {
                Id = id,
                Description = requestViewModel.Description,
                Name = requestViewModel.Name,
                Status = ProviderRequestStatus.Requested
            });
            
            await _unitOfWork.Commit();
        }
    }
}