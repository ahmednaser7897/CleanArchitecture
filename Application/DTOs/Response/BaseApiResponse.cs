
using System.Text.Json.Serialization;
namespace Application.DTOs.Response;

public class BaseApiResponse
{
    public bool Status { get; set; } = true;
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PagedResponse? Pagination { get; set; }

    public BaseApiResponse() { }

    public BaseApiResponse(int statusCode, string message, bool status = true, PagedResponse? pagination = null)
    {
        StatusCode = statusCode;
        Message = message;
        Status = status;
        Pagination = pagination;
    }
}

