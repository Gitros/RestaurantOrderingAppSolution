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
            .ForMember(dest => dest.OrderItemStatus, opt => opt.MapFrom(src => MapOrderItemStatus(src.OrderItemStatus)));

        CreateMap<OrderItemCreateDto, OrderItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.OrderItemStatus, opt => opt.MapFrom(_ => OrderItemStatus.Pending))
            .ForMember(dest => dest.OrderId, opt => opt.Ignore());

        CreateMap<OrderItemUpdateDto, OrderItem>();
    }

    private string MapOrderItemStatus(OrderItemStatus status)
    {
        return status switch
        {
            OrderItemStatus.Pending => "Pending",
            OrderItemStatus.InProgress => "In Progress",
            OrderItemStatus.ReadyToServe => "Ready to Serve",
            OrderItemStatus.Served => "Served",
            OrderItemStatus.Cancelled => "Cancelled",
            _ => "Unknown"
        };
    }
}
