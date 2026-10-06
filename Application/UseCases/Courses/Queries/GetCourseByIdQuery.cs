using Application.DTOs.Courses;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Courses.Queries;

public class GetCourseByIdQuery(int id) : IRequest<CourseDto?>
{
    public int Id { get; set; } = id;
}
public class GetCourseByIdQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetCourseByIdQueryHandler> Logger) : IRequestHandler<GetCourseByIdQuery, CourseDto?>
{
    public async Task<CourseDto?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting course with id {id}", request.Id);
        var course = await UnitOfWork.Courses.GetById(request.Id);
        return course is null ? null : CourseDto.FromEntit(course);
    }
}

