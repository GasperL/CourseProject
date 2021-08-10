using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApp.Models.Users;

namespace Core.ApplicationManagement.Services.UserService
{
    public interface IUserService
    {
        Task<UserViewModel[]> GetAllUserModels();

        Task<UserViewModel> GetUserModel(string id);
        
        Task UpdateAsync(UserViewModel userToUpdate);

        Task<IdentityResult> CreateUserViewModel(CreateUserViewModel model);
    }
}