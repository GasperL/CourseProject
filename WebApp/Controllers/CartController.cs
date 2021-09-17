using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.CartService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cart;

        public CartController(ICartService cart)
        {
            _cart = cart;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;            
            
            return View(await _cart.GetCart(userId));
        }

        public async Task<IActionResult> Add(Guid productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            await _cart.Add(productId, userId);

            return RedirectToAction("Index");
        }
        
        // [HttpPost]
        // public Task<IActionResult> Buy()
        // {
        //     return View();
        // } 
        //
        // [HttpPost]
        // public Task<IActionResult> Remove()
        // {
        //     return View();
        // } 
    }
}