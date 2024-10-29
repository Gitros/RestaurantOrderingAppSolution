using Application.Dtos.Tables;

namespace Application.Contracts;

public interface ITableService
{
    Task<TableReadDto> CreateTable(TableCreateDto tableCreateDto);
    Task<TableReadDto> GetTable(Guid id);
    Task<List<TableReadDto>> GetAllTables();
    Task<TableReadDto> UpdateTable(TableUpdateDto tableUpdateDto, Guid id);
    Task DeleteTable(Guid id);
}
