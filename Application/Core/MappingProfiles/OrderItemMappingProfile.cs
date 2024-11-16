using Application.Dtos.OrderItems;
using AutoMapper;
using Domain;

namespace Application.Core.MappingProfiles;

public class OrderItemMappingProfile : Profile
{
    public OrderItemMappingProfile()
    {
        CreateMap<OrderItem, OrderItemReadDto>()
            .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name))
            .ForMember(dest => dest.OrderItemStatus, opt => opt.MapFrom(src => src.OrderItemStatus));

        CreateMap<OrderItemCreateDto, OrderItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.OrderItemStatus, opt => opt.MapFrom(_ => OrderItemStatus.Pending));

        CreateMap<OrderItemUpdateDto, OrderItem>();
    }
}
