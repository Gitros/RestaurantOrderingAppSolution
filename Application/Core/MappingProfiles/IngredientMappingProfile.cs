﻿using Application.Dtos.Ingredients;
using AutoMapper;
using Domain;

namespace Application.Core.MappingProfiles;

public class IngredientMappingProfile : Profile
{
    public IngredientMappingProfile()
    {
        // Map from IngredientCreateDto to Ingredient
        CreateMap<IngredientCreateDto, Ingredient>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

        // Map from Ingredient to IngredientReadDto
        CreateMap<Ingredient, IngredientReadDto>();

        // Map from IngredientUpdateDto to Ingredient
        CreateMap<IngredientUpdateDto, Ingredient>();
    }
}
