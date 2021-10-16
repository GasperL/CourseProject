using System.Threading.Tasks;
using Core.ApplicationManagement.Dtos;
using Core.ApplicationManagement.Services.ApiServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductApiService _productApiService;

        public ProductsApiController(IProductApiService productApiService)
        {
            _productApiService = productApiService;
        }

        public async Task<ProductDto[]> GetProducts()
        {
            return await _productApiService.GetAvailableProducts();
        }
    } 
}