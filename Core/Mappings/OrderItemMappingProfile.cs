using AutoMapper;
using Core.Common.ViewModels;
using DataAccess.Entities;

namespace Core.Mappings
{
    public class OrderItemMappingProfile : Profile
    {
        public OrderItemMappingProfile()
        {
            CreateMap<OrderItem, OrderItemViewModel>();
        }
    }
}