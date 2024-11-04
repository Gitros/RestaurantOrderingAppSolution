using Application.Dtos.OrderItems;
using Application.Dtos.Orders;
using AutoMapper;
using Domain;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Order, OrderReadDto>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<OrderItem, OrderItemReadDto>();

        CreateMap<OrderCreateDto, Order>()
           .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<OrderItemCreateDto, OrderItem>();
    }
}
