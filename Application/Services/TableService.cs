using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.Tables;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services;

public class TableService : ITableService
{
    private readonly RestaurantOrderingContext _orderingContext;
    private readonly IMapper _mapper;

    public TableService(RestaurantOrderingContext orderingContext, IMapper mapper)
    {
        _orderingContext = orderingContext;
        _mapper = mapper;
    }

    public async Task<ResultDto<TableReadDto>> CreateTable(TableCreateDto tableCreateDto)
    {
        try
        {
            var table = _mapper.Map<Table>(tableCreateDto);

            await _orderingContext.Tables.AddAsync(table);
            await _orderingContext.SaveChangesAsync();

            var createdTableDto = _mapper.Map<TableReadDto>(table);

            return ResultDto<TableReadDto>
                .Success(createdTableDto, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return ResultDto<TableReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<TableSummaryDto>> GetTable(Guid id)
    {
        try
        {
            var table = await _orderingContext.Tables
                .Include(t => t.Orders)
                .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(t => t.Id == id && t.IsUsed && !t.IsDeleted);

            if (table == null)
                return ResultDto<TableSummaryDto>
                    .Failure("Table not found or deleted.", HttpStatusCode.NotFound);

            var tableDto = _mapper.Map<TableSummaryDto>(table);

            return ResultDto<TableSummaryDto>
                .Success(tableDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<TableSummaryDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<List<TableReadDto>>> GetAllTables()
    {
        try
        {
            var tables = await _orderingContext.Tables
            .Include(t => t.Orders)
            .Where(t => t.IsUsed && !t.IsDeleted)
            .ToListAsync();

            var tablesDto = _mapper.Map<List<TableReadDto>>(tables);

            return ResultDto<List<TableReadDto>>
                .Success(tablesDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<List<TableReadDto>>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<TableReadDto>> UpdateTable(TableUpdateDto tableUpdateDto, Guid id)
    {
        try
        {
            var tableToUpdate = await _orderingContext.Tables.FindAsync(id);

            if (tableToUpdate == null)
                return ResultDto<TableReadDto>
                    .Failure("Table not found.", HttpStatusCode.NotFound);

            _mapper.Map(tableUpdateDto, tableToUpdate);

            await _orderingContext.SaveChangesAsync();

            var updatedTable = _mapper.Map<TableReadDto>(tableToUpdate);

            return ResultDto<TableReadDto>
                .Success(updatedTable, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<TableReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<TableReadDto>> UpdateOccupancy(TableOccupancyDto tableOccupancyDto, Guid id)
    {
        try
        {
            var table = await _orderingContext.Tables.FindAsync(id);

            if (table == null)
                return ResultDto<TableReadDto>
                    .Failure("Table not found or has been deleted.", HttpStatusCode.NotFound);

            if (table.IsOccupied == tableOccupancyDto.IsOccupied)
                return ResultDto<TableReadDto>
                    .Failure("The table already has the requested occupancy status.", HttpStatusCode.BadRequest);


            table.IsOccupied = tableOccupancyDto.IsOccupied;

            await _orderingContext.SaveChangesAsync();

            var updatedTableOccupancy = _mapper.Map<TableReadDto>(table);

            return ResultDto<TableReadDto>
                .Success(updatedTableOccupancy, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<TableReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<bool>> DeleteTable(Guid id)
    {
        try
        {
            var table = await _orderingContext.Tables.FindAsync(id);
            if(table == null)
                return ResultDto<bool>
                    .Failure("Table not found.", HttpStatusCode.NotFound);

            table.IsDeleted = true;
            table.IsOccupied = false;

            _orderingContext.Tables.Update(table);
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
