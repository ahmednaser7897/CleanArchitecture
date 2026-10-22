using Domain.Entities;
using Domain.Specification.Params;

namespace Domain.Specification.Enrollments;

public class GetEnrollmentSpec : BaseSpecifications<Enrollment, int>
{
    public GetEnrollmentSpec(PaginationParams? paginationParams) : base()
    {
        if (paginationParams != null && !string.IsNullOrEmpty(paginationParams.Search))
        {
            AddCriteria(e => (e.Section != null && e.Section.Name.ToLower().Contains(paginationParams.Search.ToLower()))
                          || (e.Student != null && e.Student.User != null && e.Student.User.Name.ToLower().Contains(paginationParams.Search.ToLower())));
        }
        AddOrderBy(e => e.Id);
        AddInclude(e => e.Section);
        AddInclude(e => e.Student.User);

        if (paginationParams != null)
        {
            AddPagination(paginationParams.PageIndex, paginationParams.PageSize);
        }
    }
}
public class GetEnrollmentSpec2 : BaseSpecifications<Enrollment, int>
{
    public GetEnrollmentSpec2(int studentId, int sectionId) : base()
    {
        AddCriteria(e => e.StudentId == studentId && e.SectionId == sectionId);
    }
}
