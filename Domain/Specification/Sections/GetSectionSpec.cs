using Domain.Entities;
using Domain.Specification.Params;

namespace Domain.Specification.Sections;

public class GetSectionSpec : BaseSpecifications<Section, int>
{
    public GetSectionSpec(PaginationParams? paginationParams) : base()
    {
        if (paginationParams != null && !string.IsNullOrEmpty(paginationParams.Search))
        {
            AddCriteria(s => s.Name.ToLower().Contains(paginationParams.Search.ToLower())
                          || (s.Course != null && s.Course.Name.ToLower().Contains(paginationParams.Search.ToLower())));
        }
        AddOrderBy(s => s.Id);
        AddInclude(s => s.Course);
        AddInclude(s => s.Instructor!.User);
        AddInclude(s => s.Schedule);
        if (paginationParams != null) AddPagination(paginationParams.PageIndex, paginationParams.PageSize);
    }
}
