using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;

namespace Core.ApplicationManagement.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(string categoryName)
        {
            await _unitOfWork.Categories.Add(new Category
            {
                Id = Guid.NewGuid(),
                Name = categoryName
            });
            
            await _unitOfWork.Commit();
        }

        public async Task<CategoryViewModel[]> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAll();

            return categories.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToArray();
        }

        public async Task Remove(Guid categoryId)
        {
            await _unitOfWork.Categories.Delete(categoryId);
            await _unitOfWork.Commit();
        }
    }
}