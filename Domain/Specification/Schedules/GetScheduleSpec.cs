using Domain.Entities;
using Domain.Enums;
using Domain.Specification.Params;

namespace Domain.Specification.Schedules;

public class GetScheduleSpec : BaseSpecifications<Schedule, int>
{
    public GetScheduleSpec(PaginationParams paginationParams) : base()
    {
        if (!string.IsNullOrEmpty(paginationParams.Search))
        {
            if (Enum.TryParse<ScheduleEnum>(paginationParams.Search, true, out var scheduleEnum))
            {
                AddCriteria(s => s.Title == scheduleEnum);
            }
        }
        AddOrderBy(s => s.Id);
        AddInclude(s => s.Sections);
        AddPagination(paginationParams.PageIndex, paginationParams.PageSize);
    }
}
