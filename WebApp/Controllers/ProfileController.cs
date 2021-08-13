using System.Security.Claims;
using System.Threading.Tasks;
using Core.ApplicationManagement.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ProfileController : Controller
    {

        private readonly IUserAccountService _userAccountService;

        public ProfileController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public async Task<IActionResult> Index()
        {
            var userId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userAccountService.Get(userId);
            
            return View(user);
        }
    }
}