using System.Threading.Tasks;
using Core.Common.ViewModels.Users;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.ApplicationManagement.Services.UserService
{
    public interface IUserAccountService
    {
        Task<UserViewModel[]> GetAllUserModels();

        Task<UserViewModel> GetUserModel(string id);
        
        Task UpdateAsync(UserViewModel userToUpdate);

        Task<(IdentityResult, User)> Create(RegisterViewModel model);

        Task<SignInResult> SignIn(LoginViewModel model);
        Task SignOut();
    }
}