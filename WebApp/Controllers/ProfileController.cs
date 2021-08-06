using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}