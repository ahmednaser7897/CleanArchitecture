namespace Application.DTOs.Response;

public class PagedResponse(
    int pageIndex,
    int pageSize,
    int totalCount)
{
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
    public int TotalCount { get; set; } = totalCount;
    public bool HasNextPage => PageIndex < TotalPages;
    public bool HasPreviousPage => PageIndex > 1;
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
