using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities.Common.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<IdentityRole[]> GetRoles();

        Task<IdentityResult> CreateRole(string roleName);

        Task<IdentityRole> FindRoleById(string roleId);

        Task DeleteRole(IdentityRole role);

        Task<User[]> GetAll();

        Task<User> FindUserById(string userId);

        Task<IList<string>> GetUserRoleIds(User user);

        Task AddToRolesAsync(User user, IEnumerable<string> addedRoles);

        Task RemoveFromRolesAsync(User user, IEnumerable<string> removedRoles);

        Task Update(User user);

        Task<IdentityResult> Create(User user, string password);
        
        Task<SignInResult> SignIn(string modelEmail, string modelPassword, bool modelRememberMe, bool lockoutOnFailrule);
        
        Task SignOut();
    }
}