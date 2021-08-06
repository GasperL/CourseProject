﻿using System.Threading.Tasks;
using Core.ApplicationManagement.Services.CategoryService;
using Core.ApplicationManagement.Services.ProductGroupService;
using Core.ApplicationManagement.Services.ProductService;
using Core.Common.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _product;
        private readonly ICategoryService _category;
        private readonly IProductGroupService _group;
        private readonly ILogger<HomeController> _logger;

        public ProductsController(
            IProductService product, 
            ILogger<HomeController> logger, 
            ICategoryService category, 
            IProductGroupService @group)
        {
            _product = product;
            _logger = logger;
            _category = category;
            _group = @group;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _product.GetAll());
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _category.GetAll();
            var groups = await _group.GetAll();
            
            
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateProduct(CreatingProductOptions options)
        {
            return null();
        }
    }
}