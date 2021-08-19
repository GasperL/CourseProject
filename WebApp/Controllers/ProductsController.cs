using System.Security.Claims;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core.Common.CreateViewModels;
using Serilog;

namespace WebApp.Controllers
{
    // [Authorize(Roles = WebApplicationConstants.Roles.Provider)]
    // [Authorize(Roles = WebApplicationConstants.Roles.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductService _product;

        public ProductsController(
            IProductService product)
        {
            _product = product;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _product.GetAllProducts();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId =  User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var product = await _product.GetCreateProductViewModel(userId);
            
            return View(product);
        }

        [HttpPost]
        [ActionName("NewProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            await _product.Add(viewModel);
            Log.Information("Product created");
            
            return RedirectToAction("Index");
        }
    }
}