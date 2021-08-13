using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.ProviderService;
using Core.Common.CreateViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ProviderController : Controller
    {
        private readonly IProviderService _provider;

        public ProviderController(IProviderService provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public IActionResult CreateRequest()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            return View(new CreateProviderViewModel
            {
                UserId = userId
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(CreateProviderViewModel create)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Profile");
            }
            
            var request = await _provider.CreateRequest(create);
            
            return RedirectToAction("Requested", request);
        }

        public IActionResult Requested(Guid request)
        {
            return View(request);
        }
        
    }
}