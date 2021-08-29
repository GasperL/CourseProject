using System;
using System.Threading.Tasks;
using Core.Common.ViewModels;

namespace Core.ApplicationManagement.Services.CategoryService
{
    public interface ICategoryService
    {
       Task Create(string categoryName);

       Task<CategoryViewModel[]> GetAll();
       
       Task Remove(Guid categoryId);

       Task<CategoryViewModel> GetCategoryViewModel(Guid id);

       Task Edit(CategoryViewModel model);
    }
}