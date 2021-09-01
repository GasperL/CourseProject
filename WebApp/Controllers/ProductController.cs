using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Core.Common.CreateViewModels;
using Serilog;

namespace WebApp.Controllers
{
    // [Authorize(Roles = WebApplicationConstants.Roles.Provider)]
    // [Authorize(Roles = WebApplicationConstants.Roles.Administrator)]
    public class ProductController : Controller
    {
        private readonly IProductService _product;

        public ProductController(IProductService product)
        {
            _product = product;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _product.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid productId)
        {
            var model = await _product.GetProductViewModel(productId);
            
            return PartialView("_Details", model);
        }

        [HttpPost]
        public async Task<IActionResult> ActivateProduct(Guid productId)
        {
            await _product.Activate(productId);
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> DeactivateProduct(Guid productId)
        {
            await _product.Deactivate(productId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            return PartialView("_Create", await _product.GetCreateProductViewModel(userId));
        }

        [HttpPost]
        [ActionName("NewProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Create", viewModel);
            }

            await _product.Add(viewModel);
            
            Log.Information("Product created");

            return RedirectToAction("Index");
        }
    }
}