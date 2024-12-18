using Application.Dtos.OrderItemIngredients;
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
            .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.OrderItemIngredients
                .Where(oii => oii.Quantity > 0)
                .Select(oii => new OrderItemIngredientReadDto
                {
                    IngredientId = oii.IngredientId,
                    IngredientName = oii.Ingredient.Name,
                    Quantity = oii.Quantity,
                    Price = oii.Ingredient.Price
                }).ToList()))
            .ForMember(dest => dest.OrderItemStatus, opt => opt.MapFrom(src => MapOrderItemStatus(src.OrderItemStatus)));

        CreateMap<OrderItem, OrderItemSummaryDto>()
            .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name))
            .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.OrderItemIngredients))
            .ForMember(dest => dest.OrderItemStatus, opt => opt.MapFrom(src => MapOrderItemStatus(src.OrderItemStatus)));

        CreateMap<OrderItemCreateDto, OrderItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.OrderItemStatus, opt => opt.MapFrom(_ => OrderItemStatus.Pending))
            .ForMember(dest => dest.OrderId, opt => opt.Ignore());

        CreateMap<OrderItemUpdateDto, OrderItem>();

        CreateMap<OrderItemIngredient, OrderItemIngredientReadDto>()
            .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Ingredient.Price));

        CreateMap<OrderItemIngredientAddDto, OrderItemIngredient>();
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
