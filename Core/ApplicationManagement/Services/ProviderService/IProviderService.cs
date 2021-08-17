using System;
using System.Threading.Tasks;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;

namespace Core.ApplicationManagement.Services.ProviderService
{
    public interface IProviderService
    {
        Task CreateRequest(CreateProviderRequestViewModel requestViewModel);

        Task ApproveProviderRequest(string requestId);

        Task DeclineProviderRequest(string requestId);

        Task<ProviderRequestViewModel[]> GetAll();
    }
}