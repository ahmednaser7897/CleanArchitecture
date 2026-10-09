using Domain.Entities;
using Domain.Specification.Params;

namespace Domain.Specification.Courses;

public class GetCourseSpec : BaseSpecifications<Course, int>
{
    public GetCourseSpec(PaginationParams paginationParams) : base()
    {
        if (!string.IsNullOrEmpty(paginationParams.Search))
        {
            AddCriteria(c => c.Name.ToLower().Contains(paginationParams.Search.ToLower()));
        }
        AddOrderBy(c => c.Id);
        AddInclude(c => c.Sections);
        AddPagination(paginationParams.PageIndex, paginationParams.PageSize);
    }
}