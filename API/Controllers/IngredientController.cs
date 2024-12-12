using Application.Contracts;
using Application.Dtos.Ingredients;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class IngredientsController : BaseApiController
{
    private readonly IIngredientService _ingredientService;

    public IngredientsController(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIngredient([FromBody] IngredientCreateDto ingredientCreateDto) =>
        HandleResult(await _ingredientService.CreateIngredient(ingredientCreateDto));

    [HttpGet]
    public async Task<IActionResult> GetAllIngredients() =>
        HandleResult(await _ingredientService.GetAllIngredients());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetIngredient(Guid id) =>
        HandleResult(await _ingredientService.GetIngredient(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIngredient([FromBody] IngredientUpdateDto ingredientUpdateDto, Guid id) =>
        HandleResult(await _ingredientService.UpdateIngredient(ingredientUpdateDto, id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIngredient(Guid id) =>
        HandleResult(await _ingredientService.DeleteIngredient(id));
}
