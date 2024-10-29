using Application.Contracts;
using Application.Dtos.Tables;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class TableService : ITableService
{
    private readonly RestaurantOrderingContext _orderingContext;

    public TableService(RestaurantOrderingContext orderingContext)
    {
        _orderingContext = orderingContext;
    }

    public async Task<TableReadDto> CreateTable(TableCreateDto tableCreateDto)
    {
        var table = new Table
        {
            Id = Guid.NewGuid(),
            Name = tableCreateDto.Name,
            NumberOfPeople = tableCreateDto.NumberOfPeople,
            IsOccupied = tableCreateDto.IsOccupied,
        };

        var result = await _orderingContext.Tables.AddAsync(table);
        await _orderingContext.SaveChangesAsync();

        var createdTable = new TableReadDto
        {
            Id = result.Entity.Id,
            Name = result.Entity.Name,
            NumberOfPeople = result.Entity.NumberOfPeople,
            IsOccupied = result.Entity.IsOccupied
        };

        return createdTable;
    }

    public async Task<TableReadDto> GetTable(Guid id)
    {
        var table = await _orderingContext.Tables.FindAsync(id);

        return new TableReadDto
        {
            Id = id,
            Name = table.Name,
            NumberOfPeople = table.NumberOfPeople,
            IsOccupied = table.IsOccupied
        };
    }

    public async Task<List<TableReadDto>> GetAllTables()
    {
        var tables = await _orderingContext.Tables
            .Select(tables => new TableReadDto
            {
                Id = tables.Id,
                Name = tables.Name,
                NumberOfPeople = tables.NumberOfPeople,
                IsOccupied = tables.IsOccupied
            })
            .ToListAsync();

        return tables;
    }

    public async Task<TableReadDto> UpdateTable(TableUpdateDto tableUpdateDto, Guid id)
    {
        var tableToUpdate = await _orderingContext.Tables.FindAsync(id);

        tableToUpdate.Name = tableUpdateDto.Name;
        tableToUpdate.NumberOfPeople = tableUpdateDto.NumberOfPeople;
        tableToUpdate.IsOccupied = tableUpdateDto.IsOccupied;

        await _orderingContext.SaveChangesAsync();

        return new TableReadDto
        {
            Id = id,
            Name = tableToUpdate.Name,
            NumberOfPeople = tableToUpdate.NumberOfPeople,
            IsOccupied = tableToUpdate.IsOccupied
        };
    }

    public async Task DeleteTable(Guid id)
    {
        var tableToDelete = await _orderingContext.Tables.FindAsync(id);

        if(tableToDelete != null)
        {
            _orderingContext.Tables.Remove(tableToDelete);
            await _orderingContext.SaveChangesAsync();
        }
    }
}
