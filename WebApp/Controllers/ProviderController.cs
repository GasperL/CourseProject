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
        public async Task<IActionResult> ApproveRequest(Guid requestId)
        {
            await _provider.ApproveProviderRequest(requestId);

            return RedirectToAction("view-requests");
        }

        [HttpPost]
        public async Task<IActionResult> DeclineRequest(Guid requestId)
        {
            await _provider.DeclineProviderRequest(requestId);

            return RedirectToAction("view-requests");
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(CreateProviderViewModel create)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Profile");
            }

            try
            {
                await _provider.CreateRequest(create);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Index", "Profile");
            }
            
            return RedirectToAction("Index", "Profile");
        }

        [ActionName("view-requests")]
        public async Task <IActionResult> ProviderRequests()
        {
            return View(await _provider.GetAll());
        }
    }
}