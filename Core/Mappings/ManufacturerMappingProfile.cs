using AutoMapper;
using Core.Common.ViewModels;
using DataAccess.Entities;

namespace Core.Mappings
{
    public class ManufacturerMappingProfile : Profile
    {
        public ManufacturerMappingProfile()
        {
            CreateMap<Manufacturer, ManufacturerViewModel>();
        }
    }
}