using System.Net;
using Application.DTOs.Courses;
using Application.DTOs.Response;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Courses.Queries;

public class GetCourseByIdQuery(int id) : IRequest<BaseApiResponse<CourseDto?>>
{
    public int Id { get; set; } = id;
}
public class GetCourseByIdQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetCourseByIdQueryHandler> Logger) : IRequestHandler<GetCourseByIdQuery, BaseApiResponse<CourseDto?>>
{
    public async Task<BaseApiResponse<CourseDto?>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting course with id {id}", request.Id);
        var course = await UnitOfWork.Courses.GetById(request.Id)
        ?? throw new NotFoundException("Course", request.Id);
        return BaseApiResponse<CourseDto?>.Success(
            data: CourseDto.FromEntit(course),
            message: "Course found successfully"
        );
    }
}

