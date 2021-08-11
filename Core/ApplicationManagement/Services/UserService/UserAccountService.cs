using System.Linq;
using System.Threading.Tasks;
using Core.Common.ViewModels.Users;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
namespace Core.ApplicationManagement.Services.UserService
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserViewModel[]> GetAllUserModels()
        {
            var users = await _unitOfWork.Users.GetAll();

            var userModel =  users.Select(x => new UserViewModel
            {
                Id = x.Id,
                Email = x.Email,
            }).ToArray();

            return userModel;
        }
        
        public async Task<UserViewModel>GetUserModel(string id)
        {
            var user = await _unitOfWork.Users.FindUserById(id);

            var model = new UserViewModel
            {
                Email = user.Email,
                Id = user.Id,
                UserName = user.UserName
            };

            return model;
        }

        public async Task UpdateAsync(UserViewModel userToUpdate)
        {
            var user = await _unitOfWork.Users.FindUserById(userToUpdate.Id);
            
            if (user == null)
            {
                return;
            }
            
            await _unitOfWork.Users.Update(user);
            await _unitOfWork.Commit();
        }
        
        public async Task<(IdentityResult, User)> Create(RegisterViewModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };
                
            var result = await _unitOfWork.Users.Create(user, model.Password);

            return (result, user);
        }

        public async Task<SignInResult> SignIn(LoginViewModel model)
        {

            var result = await _unitOfWork.Users.SignIn(
                model.Username, 
                model.Password, 
                model.RememberMe, 
                false);

            return result;
        }
        
        public async Task SignOut()
        {
            await _unitOfWork.Users.SignOut();
        }
    }
}