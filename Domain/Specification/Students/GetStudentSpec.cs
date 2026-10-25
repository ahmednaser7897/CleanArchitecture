using Domain.Entities;
using Domain.Specification.Params;

namespace Domain.Specification.Students;

public class GetStudentSpec : BaseSpecifications<Student, int>
{
    public GetStudentSpec(PaginationParams? paginationParams) : base()
    {
        if (paginationParams != null && !string.IsNullOrEmpty(paginationParams.Search))
        {
            AddCriteria(s => s.User != null && s.User.Name.ToLower().Contains(paginationParams.Search.ToLower()));
        }
        AddOrderBy(s => s.Id);
        AddInclude(s => s.User);
        if (paginationParams != null) AddPagination(paginationParams.PageIndex, paginationParams.PageSize);
    }
}
public class GetStudentWuthUserSpec : BaseSpecifications<Student, int>
{
    public GetStudentWuthUserSpec(string userId) : base()
    {
        AddCriteria(s => s.UserId == userId);
        AddOrderBy(s => s.Id);
        AddInclude(s => s.User);
    }
}
