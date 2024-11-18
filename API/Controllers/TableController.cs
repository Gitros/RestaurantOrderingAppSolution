using Application.Contracts;
using Application.Dtos.Tables;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TableController : BaseApiController
{
    private readonly ITableService _tableService;

    public TableController(ITableService tableService)
    {
        _tableService = tableService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTable([FromBody] TableCreateDto tableCreateDto) =>
        HandleResult(await _tableService.CreateTable(tableCreateDto));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTable(Guid id) =>
        HandleResult(await _tableService.GetTable(id));

    [HttpGet]
    public async Task<IActionResult> GetAllTables() =>
        HandleResult(await _tableService.GetAllTables());

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTable([FromBody] TableUpdateDto tableUpdateDto, Guid id) =>
        HandleResult(await _tableService.UpdateTable(tableUpdateDto, id));

    [HttpPut("{id}/updateOccupancy")]
    public async Task<IActionResult> UpdateOccupancy([FromBody] TableOccupancyDto tableOccupancyDto, Guid id) =>
        HandleResult(await _tableService.UpdateOccupancy(tableOccupancyDto, id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTable(Guid id) =>
        HandleResult(await _tableService.DeleteTable(id));
}
