using Application.Contracts;
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
    public async Task<TableReadDto> CreateTable(TableCreateDto tableCreateDto) =>
        await _tableService.CreateTable(tableCreateDto);

    [HttpGet("{id}")]
    public async Task<TableReadDto> GetTable(Guid id) =>
        await _tableService.GetTable(id);

    [HttpGet]
    public async Task<List<TableReadDto>> GetAllTables() =>
        await _tableService.GetAllTables();

    [HttpPut("{id}")]
    public async Task<TableReadDto> UpdateTable(TableUpdateDto tableUpdateDto, Guid id) =>
        await _tableService.UpdateTable(tableUpdateDto, id);

    [HttpDelete("{id}")]
    public async Task DeleteTable(Guid id) =>
        await _tableService.DeleteTable(id);
}
