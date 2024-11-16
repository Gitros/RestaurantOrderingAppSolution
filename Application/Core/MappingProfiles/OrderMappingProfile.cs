using Application.Dtos.OrderItems;
using Application.Dtos.Orders;
using AutoMapper;
using Domain;

namespace Application.Core.MappingProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            // Order Mappings
            CreateMap<Order, OrderReadDto>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderUpdateDto, Order>();

            // OrderItem Mappings
            CreateMap<OrderItem, OrderItemReadDto>();

            CreateMap<OrderItemCreateDto, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
        }
    }
}
