using System.Threading.Tasks;
using Core.Common.ViewModels.Users;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.ApplicationManagement.Services.UserService
{
    public interface IUserAccountService
    {
        Task<UserViewModel[]> GetAll();

        Task<UserViewModel> Get(string id);
        
        Task Update(UserViewModel userToUpdate);

        Task<(IdentityResult, User)> Create(RegisterViewModel model);
    }
}