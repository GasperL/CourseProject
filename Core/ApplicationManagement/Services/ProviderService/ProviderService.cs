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

        public async Task<ProviderRequestViewModel[]> GetProviderRequests()
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
                await CreateProviderRequest(requestViewModel);
                await _unitOfWork.Commit();
                return;
            }

            if (request.Status == ProviderRequestStatus.Declined)
            {
                await UpdateProviderRequest(requestViewModel, request.Id);
            }
            else
            {
                throw new ProviderRequestException(
                    $"Cant create request because current status: {request.Status.ToString().ToLower()}");
            }

            await _unitOfWork.Commit();
        }

        private static void AssertRequestStatus(ProviderRequest request)
        {
            if (request.Status is ProviderRequestStatus.Approved or ProviderRequestStatus.Declined)
            {
                throw new ProviderRequestException($"Request status already {request.Status}")
                {
                    Status = request.Status
                };
            }
        }

        public async Task ApproveProviderRequest(string requestId)
        {
            var request = await _unitOfWork.ProviderRequest.GetEntityById(requestId);
            AssertRequestStatus(request);

            await _unitOfWork.Provider.Add(new Provider
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ProviderRequestId = request.Id,
            });

            await ChangeStatus(request, ProviderRequestStatus.Approved);
            
            await _unitOfWork.Commit();
        }

        public async Task DeclineProviderRequest(string requestId)
        {
            var request = await _unitOfWork.ProviderRequest.GetEntityById(requestId);

             AssertRequestStatus(request);

            await ChangeStatus(request, ProviderRequestStatus.Declined);

            await _unitOfWork.Commit();
        }

        private async Task ChangeStatus(ProviderRequest request, ProviderRequestStatus status)
        {
            request.Status = status;
            
            await _unitOfWork.ProviderRequest.Update(request);
        }

        private async Task<ProviderRequest?> FindProviderRequestByUser(CreateProviderRequestViewModel requestViewModel)
        {
            return await _unitOfWork.ProviderRequest.GetSingle(x => requestViewModel.UserId == x.Id);
        }

        private async Task CreateProviderRequest(CreateProviderRequestViewModel requestViewModel)
        {
            await _unitOfWork.ProviderRequest.Add(new ProviderRequest
            {
                Id = requestViewModel.UserId,
                Description = requestViewModel.Description,
                Name = requestViewModel.Name,
                Status = ProviderRequestStatus.Requested,
            });
        }

        private async Task UpdateProviderRequest(CreateProviderRequestViewModel currentRequest, string id)
        {
            var request = await _unitOfWork.ProviderRequest.GetEntityById(id);

            request.Status = ProviderRequestStatus.Requested;
            request.Description = currentRequest.Description;
            request.Name = currentRequest.Name;
            
            await _unitOfWork.ProviderRequest.Update(request);
        }
    }
}