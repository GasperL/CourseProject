using System.Threading.Tasks;
using Core.Common.Options;
using Core.Common.ViewModels;

namespace Core.ApplicationManagement.Services.CategoryService
{
    public interface ICategoryService
    {
       Task Create(CreatingCategoryOptions options);

       Task<CategoryViewModel[]> GetAll();
    }
}