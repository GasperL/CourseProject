using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.CartService;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
            
            var order = await _cart.GetCart(userId);

            if (order == null)
            {
                RedirectToAction("Index", "Home");
            }
            
            return View(order);
        }

        public async Task<IActionResult> Add(Guid productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            await _cart.Add(productId, userId);

            return NoContent();
        }
        
        [HttpPost]
        public async Task<IActionResult> Remove(Guid orderItemId)
        {
            try
            {
                await _cart.Remove(orderItemId);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                
                return NoContent();
            }
           
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