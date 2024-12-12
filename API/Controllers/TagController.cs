using Application.Contracts;
using Application.Dtos.Tags;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TagsController : BaseApiController
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag([FromBody] TagCreateDto tagCreateDto) =>
        HandleResult(await _tagService.CreateTag(tagCreateDto));

    [HttpGet]
    public async Task<IActionResult> GetAllTags() =>
        HandleResult(await _tagService.GetAllTags());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTag(Guid id) =>
        HandleResult(await _tagService.GetTag(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag([FromBody] TagUpdateDto tagUpdateDto, Guid id) =>
        HandleResult(await _tagService.UpdateTag(tagUpdateDto, id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(Guid id) =>
        HandleResult(await _tagService.DeleteTag(id));
}