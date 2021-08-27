using AutoMapper;
using Core.Common.ViewModels.Users;
using DataAccess.Entities;

namespace Core.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserViewModel>();
            
            CreateMap<RegisterViewModel, User>();
        }
    }
}