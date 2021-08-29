using System;
using System.Threading.Tasks;
using AutoMapper;
using Core.ApplicationManagement.Services.Utils;
using Core.Common.ViewModels;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;

namespace Core.ApplicationManagement.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            return _mapper.Map<CategoryViewModel[]>(categories);
        }

        public async Task Remove(Guid categoryId)
        {
            await _unitOfWork.Categories.Delete(categoryId);
            await _unitOfWork.Commit();
        }
       
        public async Task<CategoryViewModel> GetCategoryViewModel(Guid id)
        {
            var category = await _unitOfWork.Categories.GetEntityById(id);
            
            AssertionsUtils.AssertIsNotNull(category, "Категория не найдена");
           
            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task Edit(CategoryViewModel model)
        {
            var category =  await _unitOfWork.Categories.GetEntityById(model.Id);

            AssertionsUtils.AssertIsNotNull(category, "Категория не найдена");

            category.Name = model.Name;
            
            await _unitOfWork.Categories.Update(category);
            
            await _unitOfWork.Commit();
        }
    }
}