using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.ProviderService;
using Core.Common.CreateViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebApp.Controllers
{
    public class ProviderController : Controller
    {
        private readonly IProviderService _provider;
        private readonly ILogger<ProviderController> _logger;

        public ProviderController(
            IProviderService provider,
            ILogger<ProviderController> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult CreateRequest()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return View(new CreateProviderRequestViewModel
            {
                UserId = userId
            });
        }

        [HttpPost]
        public async Task<IActionResult> ApproveRequest(string requestId)
        {
            await _provider.ApproveProviderRequest(requestId);

            return RedirectToAction("view-requests");
        }

        [HttpPost]
        public async Task<IActionResult> DeclineRequest(string requestId)
        {
            await _provider.DeclineProviderRequest(requestId);

            return RedirectToAction("view-requests");
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(CreateProviderRequestViewModel create)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Profile");
            }

            try
            {
                await _provider.CreateRequest(create);
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
                
                Log.Error(exception.Message, exception);

                return View("CreateRequest",create);
            }

            return RedirectToAction("Index", "Profile");
        }

        [ActionName("view-requests")]
        public async Task<IActionResult> ProviderRequests()
        {
            return View(await _provider.GetAll());
        }
    }
}