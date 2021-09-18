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
                .ForMember(dest => dest.DiscountPrice,
                    opt 
                        => opt.MapFrom(product => ProductUtils.CalculateProductDiscountPercentages(product.Price, product.ProductGroup.Discount)))
                .ForMember(dest => dest.PhotoBase64,
                    opt 
                        => opt.MapFrom(x => FileUtils.GetPhotoBase64(x.Photos)));

            CreateMap<CreateProductViewModel, Product>()
                .ForMember(dest => dest.Photos,
                    opt => opt.Ignore());
        }
    }
}