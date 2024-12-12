using Application.Dtos.Ingredients;
using AutoMapper;
using Domain;

namespace Application.Core.MappingProfiles;

public class IngredientMappingProfile : Profile
{
    public IngredientMappingProfile()
    {
        // Map from IngredientCreateDto to Ingredient
        CreateMap<IngredientCreateDto, Ingredient>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.IsUsed, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));

        // Map from Ingredient to IngredientReadDto
        CreateMap<Ingredient, IngredientReadDto>();

        // Map from IngredientUpdateDto to Ingredient
        CreateMap<IngredientUpdateDto, Ingredient>();
    }
}
