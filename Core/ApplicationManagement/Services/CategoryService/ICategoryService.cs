using System.Threading.Tasks;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;

namespace Core.ApplicationManagement.Services.CategoryService
{
    public interface ICategoryService
    {
       Task Create(CreateCategoryViewModel viewModel);

       Task<CategoryViewModel[]> GetAll();
    }
}