
namespace Application.DTOs.Response;

public class ValidationErrorResponse : BaseApiResponse
{
    public Dictionary<string, IEnumerable<string>> Errors { get; set; } = [];
    ValidationErrorResponse() { }
    public ValidationErrorResponse(Dictionary<string, IEnumerable<string>> errors) : base(400, "Invalid data. Please correct your input and try again.")
    {
        Errors = errors;
    }

}
