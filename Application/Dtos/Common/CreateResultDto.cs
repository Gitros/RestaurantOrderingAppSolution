namespace Application.Dtos.Common;

public class CreateResultDto<T> : ResultDto
{
    public T EntityResult { get; set; }
}
