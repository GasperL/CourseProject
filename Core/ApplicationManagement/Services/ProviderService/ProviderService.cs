﻿#nullable enable
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProviderService(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProviderRequestViewModel[]> GetProviderRequests()
        {
            var requests = await _unitOfWork.ProviderRequests.GetList(
                isTracking: false,
                selector: s => s,
                filter: x => x.Status == ProviderRequestStatus.Requested);
            
            return _mapper.Map<ProviderRequestViewModel[]>(requests);
        }

        public async Task CreateRequest(CreateProviderRequestViewModel requestViewModel)
        {
            var request = await GetProviderRequestByUser(requestViewModel);

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
            var request = await _unitOfWork.ProviderRequests.GetEntityById(requestId);
            AssertRequestStatus(request);

            await _unitOfWork.Providers.Add(_mapper.Map<Provider>(request));
            await ChangeStatus(request, ProviderRequestStatus.Approved);
            await _unitOfWork.Commit();
        }

        public async Task DeclineProviderRequest(string requestId)
        {
            var request = await _unitOfWork.ProviderRequests.GetEntityById(requestId);

             AssertRequestStatus(request);

            await ChangeStatus(request, ProviderRequestStatus.Declined);

            await _unitOfWork.Commit();
        }

        private async Task ChangeStatus(ProviderRequest request, ProviderRequestStatus status)
        {
            request.Status = status;
            
            await _unitOfWork.ProviderRequests.Update(request);
        }

        private async Task<ProviderRequest?> GetProviderRequestByUser(CreateProviderRequestViewModel requestViewModel)
        {
            return await _unitOfWork.ProviderRequests.GetSingleOrDefault(
                isTracking: false,
                selector: s => s,
                filter: x => requestViewModel.UserId == x.Id);
        }

        private async Task CreateProviderRequest(CreateProviderRequestViewModel requestViewModel)
        {
            var request = _mapper.Map<ProviderRequest>(requestViewModel);
            request.Status = ProviderRequestStatus.Requested;
            
            await _unitOfWork.ProviderRequests.Add(request);
        }

        private async Task UpdateProviderRequest(CreateProviderRequestViewModel currentRequest, string id)
        {
            var request = await _unitOfWork.ProviderRequests.GetEntityById(id);

            request.Status = ProviderRequestStatus.Requested;
            request.Description = currentRequest.Description;
            request.Name = currentRequest.Name;
            
            await _unitOfWork.ProviderRequests.Update(request);
        }
    }
}