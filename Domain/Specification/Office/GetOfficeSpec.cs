using Domain.Entities;
using Domain.Specification.Params;

namespace Domain.Specification.Courses;

public class GetOfficeSpec : BaseSpecifications<Office, int>
{
    public GetOfficeSpec(PaginationParams paginationParams) : base()
    {
        if (!string.IsNullOrEmpty(paginationParams.Search))
        {
            AddCriteria(c => c.Name.ToLower().Contains(paginationParams.Search.ToLower()));
        }
        AddOrderBy(c => c.Id);
        AddInclude(c => c.Instructor);
        AddPagination(paginationParams.PageIndex, paginationParams.PageSize);
    }
}