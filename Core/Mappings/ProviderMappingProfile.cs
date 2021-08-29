using System;
using AutoMapper;
using Core.Common.CreateViewModels;
using Core.Common.ViewModels;
using DataAccess.Entities;

namespace Core.Mappings
{
    public class ProviderMappingProfile : Profile
    {
        public ProviderMappingProfile()
        {
            CreateMap<ProviderRequest, ProviderRequestViewModel>();

            CreateMap<ProviderRequest, Provider>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(x => Guid.NewGuid()))
                .ForMember(dest => dest.ProviderRequestId,
                    opt => opt.MapFrom(x => x.Id));

            CreateMap<CreateProviderRequestViewModel, ProviderRequest>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(x => x.UserId));
        }
    }
}