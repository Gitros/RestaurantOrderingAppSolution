using Application.Dtos.Common;
using System.Linq.Expressions;

namespace Application.Contracts;

public interface IGenericCrudService<TModel, TDto>
{
    Task<ResultDto<TDto>> GetByIdAsync(Expression<Func<TModel, bool>> predicateToGetId, params string[] includes);
    Task<ResultDto<IEnumerable<TDto>>> GetAllAsync(Expression<Func<TModel, bool>>? where = null, params string[] includes);
    Task<ResultDto<TDto>> UpdateAsync(TDto dto, Expression<Func<TModel, bool>>? where = null, params Expression<Func<TModel, object>>[] references);
    Task<ResultDto<bool>> DeleteAsync(Guid id);
}
