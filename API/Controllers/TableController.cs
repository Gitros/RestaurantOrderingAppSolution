using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.Tables;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TableController : ControllerBase
{
    private readonly ITableService _tableService;

    public TableController(ITableService tableService)
    {
        _tableService = tableService;
    }

    [HttpPost]
    public async Task<ResultDto<TableReadDto>> CreateTable(TableCreateDto tableCreateDto) =>
        await _tableService.CreateTable(tableCreateDto);

    [HttpGet("{id}")]
    public async Task<ResultDto<TableReadDto>> GetTable(Guid id) =>
        await _tableService.GetTable(id);

    [HttpGet]
    public async Task<ResultDto<List<TableReadDto>>> GetAllTables() =>
        await _tableService.GetAllTables();

    [HttpPut("{id}")]
    public async Task<ResultDto<TableReadDto>> UpdateTable(TableUpdateDto tableUpdateDto, Guid id) =>
        await _tableService.UpdateTable(tableUpdateDto, id);

    [HttpPut("{id}/updateOccupancy")]
    public async Task<ResultDto<TableReadDto>> UpdateOccupancy(TableOccupancyDto tableOccupancyDto, Guid id) =>
        await _tableService.UpdateOccupancy(tableOccupancyDto, id);

    [HttpDelete("{id}")]
    public async Task<ResultDto<bool>> DeleteTable(Guid id) =>
        await _tableService.DeleteTable(id);
}
