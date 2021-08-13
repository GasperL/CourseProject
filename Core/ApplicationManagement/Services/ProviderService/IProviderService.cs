using System;
using System.Threading.Tasks;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;

namespace Core.ApplicationManagement.Services.ProviderService
{
    public interface IProviderService
    {
        Task<ProviderRequestViewModel[]> GetAllRequests();

        Task<ProviderRequestViewModel> GetRequestModel(Guid id);

        Task<Guid> CreateRequest(CreateProviderViewModel viewModel);

        Task ApproveProvider(Guid requestId);

        Task DeclineProvider(Guid requestId);
    }
}