using System.Threading.Tasks;
using AutoMapper;
using Core.Common.ViewModels.Users;
using DataAccess.Entities;
using DataAccess.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Core.ApplicationManagement.Services.UserService
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserAccountService(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserViewModel[]> GetAll()
        {
            var users = await _unitOfWork.Users.GetAll();

            return _mapper.Map<UserViewModel[]>(users);
        }

        public async Task<UserViewModel> Get(string id)
        {
            var user = await _unitOfWork.Users.FindUserById(id);

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task Update(UserViewModel userToUpdate)
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
            var user = _mapper.Map<User>(model);

            var result = await _unitOfWork.Users.Create(user, model.Password);

            return (result, user);
        }
    }
}