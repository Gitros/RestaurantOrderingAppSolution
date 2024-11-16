using Application.Dtos.MenuItems;
using Application.Dtos.MenuTypes;
using AutoMapper;
using Domain;

namespace Application.Core.MappingProfiles;

public class MenuMappingProfile : Profile
{
    public MenuMappingProfile()
    {
        // MenuType Mappings
        CreateMap<MenuType, MenuTypeReadDto>()
            .ForMember(dest => dest.MenuItems, opt => opt.MapFrom(src => src.MenuItems));

        CreateMap<MenuTypeCreateDto, MenuType>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));

        CreateMap<MenuTypeUpdateDto, MenuType>();

        // MenuItem Mappings
        CreateMap<MenuItem, MenuItemReadDto>()
            .ForMember(dest => dest.MenuTypeName, opt => opt.MapFrom(src => src.MenuType.Name));

        CreateMap<MenuItemCreateDto, MenuItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));

        CreateMap<MenuItemUpdateDto, MenuItem>();
    }
}
