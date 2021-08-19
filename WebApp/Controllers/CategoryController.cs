using System;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.CategoryService;
using Core.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _category;

        public CategoryController(ICategoryService category)
        {
            _category = category;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _category.GetAll();
            
            return View(categories);
        }

        public IActionResult Add()
        {
            return PartialView("_Add", new CategoryViewModel());
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Add", model);
            }
            
            await _category.Create(model.Name);
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> Remove(Guid categoryId)
        {
            await _category.Remove(categoryId);
            
            return RedirectToAction("Index");
        }
    }
}