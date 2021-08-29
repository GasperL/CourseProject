using AutoMapper;
using Core.Common.ViewModels;
using DataAccess.Entities;

namespace Core.Mappings
{
    public class ProductGroupMappingProfile : Profile
    {
        public ProductGroupMappingProfile()
        {
            CreateMap<ProductGroup, ProductGroupViewModel>();
        }
    }
}