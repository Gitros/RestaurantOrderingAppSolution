using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.Ingredients;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services;

public class IngredientService : IIngredientService
{
    private RestaurantOrderingContext _orderingContext;
    private readonly IMapper _mapper;

    public IngredientService(RestaurantOrderingContext orderingContext, IMapper mapper)
    {
        _orderingContext = orderingContext;
        _mapper = mapper;
    }

    public async Task<ResultDto<IngredientReadDto>> CreateIngredient(IngredientCreateDto ingredientCreateDto)
    {
        try
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientCreateDto);

            await _orderingContext.Ingredients.AddAsync(ingredient);
            await _orderingContext.SaveChangesAsync();

            var createdIngredient = _mapper.Map<IngredientReadDto>(ingredient);

            return ResultDto<IngredientReadDto>
                .Success(createdIngredient, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return ResultDto<IngredientReadDto>
                .Failure($"An error occured: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<List<IngredientReadDto>>> GetAllIngredients()
    {
        try
        {
            var ingredients = await _orderingContext.Ingredients
                .ToListAsync();

            var ingredientDtos = _mapper.Map<List<IngredientReadDto>>(ingredients);

            return ResultDto<List<IngredientReadDto>>
                .Success(ingredientDtos, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<List<IngredientReadDto>>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<IngredientReadDto>> GetIngredient(Guid id)
    {
        try
        {
            var ingredient = await _orderingContext.Ingredients
                .FirstOrDefaultAsync(i => i.Id == id);

            if (ingredient == null)
                return ResultDto<IngredientReadDto>
                    .Failure("Ingredient not found.", HttpStatusCode.NotFound);

            var ingredientDto = _mapper.Map<IngredientReadDto>(ingredient);

            return ResultDto<IngredientReadDto>
                .Success(ingredientDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<IngredientReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<IngredientReadDto>> UpdateIngredient(IngredientUpdateDto ingredientUpdateDto, Guid id)
    {
        try
        {
            var ingredient = await _orderingContext.Ingredients.FindAsync(id);

            if (ingredient == null)
                return ResultDto<IngredientReadDto>
                    .Failure("Ingredient not found.", HttpStatusCode.NotFound);

            _mapper.Map(ingredientUpdateDto, ingredient);
            await _orderingContext.SaveChangesAsync();

            var updatedIngredientDto = _mapper.Map<IngredientReadDto>(ingredient);

            return ResultDto<IngredientReadDto>
                .Success(updatedIngredientDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<IngredientReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<bool>> DeleteIngredient(Guid id)
    {
        try
        {
            var ingredient = await _orderingContext.Ingredients.FindAsync(id);

            if (ingredient == null)
                return ResultDto<bool>
                    .Failure("Ingredient not found.", HttpStatusCode.NotFound);

            ingredient.IsDeleted = true;
            ingredient.IsUsed = false;
            await _orderingContext.SaveChangesAsync();

            return ResultDto<bool>
                .Success(true, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<bool>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}
