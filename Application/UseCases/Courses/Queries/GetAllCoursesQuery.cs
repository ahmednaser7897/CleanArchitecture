using Application.DTOs.Courses;
using Domain.Entities;
using Domain.Interfaces.Specification;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Courses.Queries;

public class GetAllCoursesQuery : IRequest<List<CourseDto>>;

public class GetAllCoursesQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetAllCoursesQueryHandler> Logger) : IRequestHandler<GetAllCoursesQuery, List<CourseDto>>
{
    public async Task<List<CourseDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting all courses");
        var courses = (await UnitOfWork.Courses.GetAllWithSpec(new GetCourseSpec()))
                      .Select(c => CourseDto.FromEntit(c));
        return [.. courses];
    }
}

public class GetCourseSpec : BaseSpecifications<Course, int>
{
    public GetCourseSpec() : base()
    {
        AddInclude(c => c.Sections);
    }
}

