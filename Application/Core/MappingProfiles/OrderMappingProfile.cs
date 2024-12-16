using Application.Dtos.Orders;
using AutoMapper;
using Domain;

namespace Application.Core.MappingProfiles;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderReadDto>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()))
            .ForMember(dest => dest.OrderType, opt => opt.MapFrom(src => src.OrderType.ToString()))
            .ForMember(dest => dest.TableId, opt => opt.MapFrom(src => src.OrderType == OrderType.DineIn ? src.TableId : null))
            .ForMember(dest => dest.DeliveryAddress, opt => opt.MapFrom(src => src.OrderType == OrderType.Delivery ? src.DeliveryAddress : null));

        CreateMap<Order, OrderSummaryDto>()
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()))
            .ForMember(dest => dest.OrderType, opt => opt.MapFrom(src => src.OrderType.ToString()));

        CreateMap<OrderCreateDto, Order>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(_ => OrderStatus.Pending))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.OrderItems, opt => opt.Ignore());

        CreateMap<OrderUpdateDto, Order>();
    }
}
