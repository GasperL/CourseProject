using System.Threading.Tasks;
using Core.Common.Options;
using Core.Common.ViewModels;

namespace Core.ApplicationManagement.Services.ProductGroupService
{
    public interface IProductGroupService 
    {
        Task Create(CreatingProductGroupOptions options);

        Task<ProductGroupViewModel[]> GetAll();
    }
}