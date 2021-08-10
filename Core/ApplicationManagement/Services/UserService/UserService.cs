using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using WebApp.Models.Users;

namespace Core.ApplicationManagement.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
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
        
        public Task<IdentityResult> Create(CreateUserViewModel model)
        {
            var user = new User
            {
                Email = model.Email,
            };
                
            return _unitOfWork.Users.Create(user, model.Password);
        }
    }
}