using AutoMapper;
using Core.ApplicationManagement.Dtos;
using Core.Common.ViewModels;
using DataAccess.Entities;

namespace Core.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, CategoryDto>();
        }
    }
}