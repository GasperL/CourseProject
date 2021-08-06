using System.Threading.Tasks;
using Core.Common.Options;
using Core.Common.ViewModels;

namespace Core.ApplicationManagement.Services.ProviderService
{
    public interface IProviderService
    {
        Task Create(CreatingProviderOptions options);
        
        Task<ProviderViewModel[]> GetAll();
    }
}