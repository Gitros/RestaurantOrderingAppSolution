using Application.Dtos.MenuTypes;
using AutoMapper;
using Domain;

namespace Application.Core.MappingProfiles;

public class MenuMappingProfile : Profile
{
    public MenuMappingProfile()
    {
        CreateMap<MenuType, MenuTypeReadDto>()
            .ForMember(dest => dest.MenuItems, opt => opt.MapFrom(src => src.MenuItems));

        CreateMap<MenuTypeCreateDto, MenuType>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));

        CreateMap<MenuTypeUpdateDto, MenuType>();
    }
}
