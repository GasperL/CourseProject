using System.Threading.Tasks;
using Core.ApplicationManagement.Dtos;
using Core.ApplicationManagement.Services.ApiServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoriesApiService _categoriesApiService;

        public CategoriesApiController(ICategoriesApiService categoriesApiService)
        {
            _categoriesApiService = categoriesApiService;
        }

        public async Task<CategoryDto[]> GetCategories()
        {
            return await _categoriesApiService.GetCategories();
        }
    } 
}