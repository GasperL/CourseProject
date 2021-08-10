using System.Threading.Tasks;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels.Users;
using Microsoft.AspNetCore.Identity;

namespace Core.ApplicationManagement.UserAccountService
{
    public interface IUserService
    {
        Task<UserViewModel[]> GetAllUserModels();

        Task<UserViewModel> GetUserModel(string id);
        
        Task UpdateAsync(UserViewModel userToUpdate);

        Task<IdentityResult> Create(CreateUserViewModel model);
    }
}