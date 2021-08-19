using System;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.ManufacturerService;
using Core.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService _manufacturer;

        public ManufacturerController(IManufacturerService manufacturer)
        {
            _manufacturer = manufacturer;
        }

        public async Task<IActionResult> Index()
        {
            var manufacturers = await _manufacturer.GetAll();
            
            return View(manufacturers);
        }

        public IActionResult Add()
        {
            return PartialView("_Add", new ManufacturerViewModel());
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(ManufacturerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Add", model);
            }
            
            await _manufacturer.Create(model);
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> Remove(Guid categoryId)
        {
            await _manufacturer.Remove(categoryId);
            
            return RedirectToAction("Index");
        }
    }
}