using Application.Contracts;
using Application.Dtos.Common;
using AutoMapper;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services;

public class GenericCrudService<T, TReadDto, TCreateDto, TUpdateDto> :
    IGenericCrudService<T, TReadDto, TCreateDto, TUpdateDto> where T : class
{
    private readonly RestaurantOrderingContext _orderingContext;
    private readonly IMapper _mapper;
    private readonly DbSet<T> _dbSet;

    public GenericCrudService(RestaurantOrderingContext orderingContext, IMapper mapper)
    {
        _orderingContext = orderingContext;
        _mapper = mapper;
        _dbSet = _orderingContext.Set<T>();
    }

    public async Task<ResultDto<TReadDto>> CreateAsync(TCreateDto createDto)
    {
        var entity = _mapper.Map<T>(createDto);
        await _dbSet.AddAsync(entity);
        await _orderingContext.SaveChangesAsync();

        var dto = _mapper.Map<TReadDto>(entity);
        return ResultDto<TReadDto>.Success(dto, HttpStatusCode.Created);
    }

    public async Task<ResultDto<TReadDto>> GetByIdAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);

        if(entity == null) 
            return ResultDto<TReadDto>.Failure("Entity not found", HttpStatusCode.NotFound);

        var dto = _mapper.Map<TReadDto>(entity);
        return ResultDto<TReadDto>.Success(dto, HttpStatusCode.OK);
    }

    public Task<ResultDto<List<TReadDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResultDto<TReadDto>> UpdateAsync(TUpdateDto updateDto, Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ResultDto<string>> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
