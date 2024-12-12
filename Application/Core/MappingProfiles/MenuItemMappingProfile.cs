﻿using Application.Dtos.MenuItems;
using AutoMapper;
using Domain;

namespace Application.Core.MappingProfiles;

public class MenuItemMappingProfile : Profile
{
    public MenuItemMappingProfile()
    {
        CreateMap<MenuItem, MenuItemReadDto>();

        CreateMap<MenuItem, MenuItemDetailedDto>()
            .ForMember(dest => dest.MenuCategoryName, opt => opt.MapFrom(src => src.MenuCategory.Name));

        CreateMap<MenuItemCreateDto, MenuItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));

        CreateMap<MenuItemUpdateDto, MenuItem>();
    }
}
