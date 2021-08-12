﻿using System;
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

        public async Task Create(CreateCategoryViewModel viewModel)
        {
            var id = Guid.NewGuid();
            await _unitOfWork.Categories.Add(new Category
            {
                Id = id,
                Name = viewModel.Name
            });
        }

        public async Task<CategoryViewModel[]> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAll();

            var categoryViewModels = categories.Select(x => new CategoryViewModel
            {
                   Name = x.Name
            }).ToArray();

            return categoryViewModels;
        }
    }
}