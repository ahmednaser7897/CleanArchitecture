using Domain.Entities;
using Domain.Specification.Params;

namespace Domain.Specification.Instructors;

public class GetInstructorSpec : BaseSpecifications<Instructor, int>
{
    public GetInstructorSpec(PaginationParams? paginationParams) : base()
    {
        if (paginationParams != null && !string.IsNullOrEmpty(paginationParams.Search))
        {
            AddCriteria(i => i.User != null && i.User.Name.ToLower().Contains(paginationParams.Search.ToLower()));
        }
        AddOrderBy(i => i.Id);
        AddInclude(i => i.Office);
        AddInclude(i => i.User);
        if (paginationParams != null) AddPagination(paginationParams.PageIndex, paginationParams.PageSize);
    }
}
public class GetInstructorWuthUserSpec : BaseSpecifications<Instructor, int>
{
    public GetInstructorWuthUserSpec(string userId) : base()
    {
        AddCriteria(i => i.UserId == userId);
        AddOrderBy(i => i.Id);
        AddInclude(s => s.User);
    }
}