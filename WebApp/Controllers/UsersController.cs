using System.Threading.Tasks;
using Core.ApplicationManagement.Services.UserService;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Users;

namespace WebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetAllUserModels());
        }


        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userModel = await _userService.GetUserModel(id);
            
            if (userModel == null)
            {
                return BadRequest();
            }

            return View("UserDetails", userModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var userModel = await _userService.GetUserModel(id);
            
            if (userModel == null)
            {
                return BadRequest();
            }

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userToUpdate = await _userService.GetUserModel(userViewModel.Id);
            
            if (userToUpdate == null)
            {
                return BadRequest();
            }

            userToUpdate.Email = userViewModel.Email;
            
            await _userService.UpdateAsync(userToUpdate);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.Create(model);
            
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, $"(${error.Code}) ${error.Description})");
            }
            
            return View(model);
        }
    }
}
