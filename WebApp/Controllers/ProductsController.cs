﻿using System.Threading.Tasks;
using Core.ApplicationManagement.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CreateProductViewModel = Core.Common.CreateViewModels.CreateProductViewModel;

namespace WebApp.Controllers
{
    // [Authorize(Roles = WebApplicationConstants.Roles.Provider)]
    // [Authorize(Roles = WebApplicationConstants.Roles.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductService _product;
        private readonly ILogger<HomeController> _logger;

        public ProductsController(
            IProductService product,
            ILogger<HomeController> logger
        )
        {
            _product = product;
            _logger = logger;
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
            // var userId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //
            // if (!User.IsInRole(WebApplicationConstants.Roles.Provider) || userId == null)
            // {
            //     RedirectToAction("Index", "Home");
            // }
            
            var product = await _product.CreateProductViewModel();
            
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> NewProduct(CreateProductViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            await _product.Add(viewModel);

            return RedirectToAction("Index");
        }
    }
}