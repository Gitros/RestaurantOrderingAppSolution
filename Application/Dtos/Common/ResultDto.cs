using System.Net;

namespace Application.Dtos.Common;

public abstract class ResultDto
{
    public bool IsSuccess { get; set; } = true;
    public string ErrorMessage { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

}
