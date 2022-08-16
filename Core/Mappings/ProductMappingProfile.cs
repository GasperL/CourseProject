using System.Linq;
using AutoMapper;
using Core.ApplicationManagement.Services.Utils;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels.MainEntityViewModels;
using DataAccess.Entities;

namespace Core.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
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

            CreateMap<CreateProductViewModel, Product>()
                .ForMember(dest => dest.Photos,
                    opt => opt.Ignore());
        }
    }
}