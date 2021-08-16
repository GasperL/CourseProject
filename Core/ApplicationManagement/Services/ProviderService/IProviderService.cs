using System;
using System.Threading.Tasks;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;

namespace Core.ApplicationManagement.Services.ProviderService
{
    public interface IProviderService
    {
        Task<ProviderRequestViewModel> GetRequestModel(Guid id);

        Task CreateRequest(CreateProviderViewModel viewModel);

        Task ApproveProviderRequest(Guid requestId);

        Task DeclineProviderRequest(Guid requestId);

        Task<ProviderRequestViewModel[]> GetAll();
    }
}