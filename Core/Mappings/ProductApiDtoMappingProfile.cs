using System.Linq;
using AutoMapper;
using Core.ApplicationManagement.Dtos;
using Core.ApplicationManagement.Services.Utils;
using DataAccess.Entities;

namespace Core.Mappings
{
    public class ProductApiDtoMappingProfile : Profile
    {
        public ProductApiDtoMappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Price,
                    opt
                        => opt.MapFrom(x => x.Price))
                .ForMember(dest => dest.DiscountPrice,
                    opt
                        => opt.MapFrom(product =>
                            ProductUtils.CalculateProductDiscountPercentages(product.Price,
                                product.ProductGroup.Discount)))
                .ForMember(dest => dest.PhotoBase64,
                    opt
                        => opt.MapFrom(x => FileUtils.GetPhotoBase64(x.Photos.Select(f => f.Image))))
                .ForMember(dest => dest.CoverPhotoBase64,
                    opt
                        => opt.MapFrom(f => FileUtils.GetPhotoBase64(f.CoverPhoto.Image)))
                .ForMember(dest => dest.ManufacturerName,
                    opt
                        => opt.MapFrom(m => m.Manufacturer.Name))
                .ForMember(dest => dest.CategoryName,
                    opt
                        => opt.MapFrom(m => m.Category.Name))
                .ForMember(dest => dest.ProductGroupName,
                    opt
                        => opt.MapFrom(m => m.ProductGroup.Name))
                .ForMember(dest => dest.DiscountPercentages,
                    opt
                        => opt.MapFrom(m => m.ProductGroup.Discount));
        }
    }
}