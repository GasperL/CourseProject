using AutoMapper;
using Core.Common.ViewModels;
using DataAccess.Entities;

namespace Core.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}