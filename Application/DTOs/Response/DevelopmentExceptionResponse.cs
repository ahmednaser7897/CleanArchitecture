namespace Application.DTOs.Response;

public class DevelopmentExceptionResponse : BaseApiResponse
{
    public string? StackTrace { get; set; }
    public string? InnerException { get; set; }
    public string? InnerStackTrace { get; set; }
    public string? Path { get; set; }
}
