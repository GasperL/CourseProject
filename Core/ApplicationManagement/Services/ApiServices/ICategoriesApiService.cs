using System.Threading.Tasks;
using Core.ApplicationManagement.Dtos;

namespace Core.ApplicationManagement.Services.ApiServices
{
    public interface ICategoriesApiService
    {
        Task<CategoryDto[]> GetCategories();
    }
}