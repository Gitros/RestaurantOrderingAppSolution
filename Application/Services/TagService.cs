using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.Tags;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services;

public class TagService : ITagService
{
    private readonly RestaurantOrderingContext _orderingContext;
    private readonly IMapper _mapper;

    public TagService(RestaurantOrderingContext orderingContext, IMapper mapper)
    {
        _orderingContext = orderingContext;
        _mapper = mapper;
    }

    public async Task<ResultDto<TagReadDto>> CreateTag(TagCreateDto tagCreateDto)
    {
        try
        {
            var tag = _mapper.Map<Tag>(tagCreateDto);

            await _orderingContext.Tags.AddAsync(tag);
            await _orderingContext.SaveChangesAsync();

            var createdTag = _mapper.Map<TagReadDto>(tag);

            return ResultDto<TagReadDto>
                .Success(createdTag, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return ResultDto<TagReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<List<TagReadDto>>> GetAllTags()
    {
        try
        {
            var tags = await _orderingContext.Tags
                .ToListAsync();

            var tagDtos = _mapper.Map<List<TagReadDto>>(tags);

            return ResultDto<List<TagReadDto>>
                .Success(tagDtos, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<List<TagReadDto>>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<TagReadDto>> GetTag(Guid id)
    {
        try
        {
            var tag = await _orderingContext.Tags
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tag == null)
                return ResultDto<TagReadDto>
                    .Failure("Tag not found.", HttpStatusCode.NotFound);

            var tagDto = _mapper.Map<TagReadDto>(tag);

            return ResultDto<TagReadDto>
                .Success(tagDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<TagReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<TagReadDto>> UpdateTag(TagUpdateDto tagUpdateDto, Guid id)
    {
        try
        {
            var tag = await _orderingContext.Tags.FindAsync(id);

            if (tag == null)
                return ResultDto<TagReadDto>
                    .Failure("Tag not found.", HttpStatusCode.NotFound);

            _mapper.Map(tagUpdateDto, tag);
            await _orderingContext.SaveChangesAsync();

            var updatedTag = _mapper.Map<TagReadDto>(tag);

            return ResultDto<TagReadDto>
                .Success(updatedTag, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<TagReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<bool>> DeleteTag(Guid id)
    {
        try
        {
            var tag = await _orderingContext.Tags.FindAsync(id);

            if (tag == null)
                return ResultDto<bool>
                    .Failure("Tag not found.", HttpStatusCode.NotFound);

            tag.IsDeleted = true;
            tag.IsUsed = false;
            await _orderingContext.SaveChangesAsync();

            return ResultDto<bool>.Success(true, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<bool>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}
