using AutoMapper;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Mappings
{
    public class SelectListItemProfile : Profile
    {
        public SelectListItemProfile()
        {
            CreateMap<Manufacturer, SelectListItem>()
                .ForMember(dest => dest.Value, 
                    opt => opt.MapFrom(x => x.Id.ToString()))
                .ForMember(dest => dest.Text, 
                    opt => opt.MapFrom(x => x.Name));
            
            CreateMap<Category, SelectListItem>()
                .ForMember(dest => dest.Value, 
                    opt => opt.MapFrom(x => x.Id.ToString())) 
                .ForMember(dest => dest.Text, 
                    opt => opt.MapFrom(x => x.Name));
           
            CreateMap<ProductGroup, SelectListItem>()
                .ForMember(dest => dest.Value, 
                    opt => opt.MapFrom(x => x.Id.ToString())) 
                .ForMember(dest => dest.Text, 
                    opt => opt.MapFrom(x => x.Name));
        }
    }
}