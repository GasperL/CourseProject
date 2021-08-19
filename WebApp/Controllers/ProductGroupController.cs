using System;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.ProductGroupService;
using Core.Common.CreateViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    
    public class ProductGroupController : Controller
    {
        private readonly IProductGroupService _productGroup;

        public ProductGroupController(IProductGroupService productGroup)
        {
            _productGroup = productGroup;
        }

        public async Task<IActionResult> Index()
        {
            var productGroups = await _productGroup.GetAll();
            
            return View(productGroups);
        }

        public IActionResult Add()
        {
            return PartialView("_Add", new CreateProductGroupViewModel());
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(CreateProductGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Add", model);
            }
            
            await _productGroup.Create(model);
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> Remove(Guid categoryId)
        {
            await _productGroup.Remove(categoryId);
            
            return RedirectToAction("Index");
        }
    }
}