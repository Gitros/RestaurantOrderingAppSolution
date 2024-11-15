using Application.Dtos.MenuItems;
using Application.Dtos.MenuTypes;
using Application.Dtos.OrderItems;
using Application.Dtos.Orders;
using AutoMapper;
using Domain;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Mapping for order
        CreateMap<Order, OrderReadDto>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<OrderItem, OrderItemReadDto>();

        CreateMap<OrderCreateDto, Order>()
           .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<OrderItemCreateDto, OrderItem>();

        CreateMap<OrderUpdateDto, Order>();

        // Mapping for Menu
        CreateMap<MenuType, MenuTypeReadDto>()
            .ForMember(dest => dest.MenuItems, opt => opt.MapFrom(src => src.MenuItems));

        CreateMap<MenuItem, MenuItemReadDto>()
            .ForMember(dest => dest.MenuTypeName, opt => opt.MapFrom(src => src.MenuType.Name));

        CreateMap<MenuTypeCreateDto, MenuType>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

        CreateMap<MenuTypeUpdateDto, MenuType>();

        CreateMap<MenuItemCreateDto, MenuItem>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

        CreateMap<MenuItemUpdateDto, MenuItem>();
    }
}
