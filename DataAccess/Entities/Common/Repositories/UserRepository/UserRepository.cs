using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities.Common.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public Task<IdentityRole[]> GetRoles()
        {
            return _roleManager.Roles.ToArrayAsync();
        }

        public Task<IdentityResult> CreateRole(string roleName)
        {
            return _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public Task<IdentityRole> FindRoleById(string roleId)
        {
            return _roleManager.FindByIdAsync(roleId);
        }

        public Task DeleteRole(IdentityRole role)
        {
            return _roleManager.DeleteAsync(role);
        }

        public Task<User[]> GetAll()
        {
            return _userManager.Users.ToArrayAsync();
        }

        public Task<User> FindUserById(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public Task<IList<string>> GetUserRoleIds(User user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public Task AddToRolesAsync(User user, IEnumerable<string> addedRoles)
        {
            return _userManager.AddToRolesAsync(user, addedRoles);
        }

        public Task RemoveFromRolesAsync(User user, IEnumerable<string> removedRoles)
        {
            return _userManager.RemoveFromRolesAsync(user, removedRoles);
        }

        public Task Update(User user)
        {
            return _userManager.UpdateAsync(user);
        }

        public Task<IdentityResult> Create(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<SignInResult> SignIn(
            string modelEmail,
            string modelPassword,
            bool modelRememberMe,
            bool lockoutOnFailure)
        {
            var result = _signInManager.PasswordSignInAsync(
                modelEmail,
                modelPassword,
                modelRememberMe, lockoutOnFailure);

            return result;
        }

        public Task SignOut()
        {
            _signInManager.SignOutAsync();

            return Task.CompletedTask;
        }
    }
}