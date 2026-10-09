namespace Domain.Specification.Params;

public class PaginationParams
{
    public string? Search { get; set; }
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
}
