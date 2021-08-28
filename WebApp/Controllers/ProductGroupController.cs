using System;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.ProductGroupService;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;
using DataAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
        
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _productGroup.GetProductGroupViewModel(id);
            
            return PartialView("_Edit", model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(ProductGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Edit", model);
            }
           
            await _productGroup.Edit(model);
            
            Log.Information($"Product id {model.Id} edited");
            
            return RedirectToAction("Index");
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