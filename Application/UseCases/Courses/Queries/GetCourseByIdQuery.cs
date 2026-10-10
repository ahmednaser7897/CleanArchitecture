using Application.DTOs.Courses;
using Application.DTOs.Response;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using Domain.Entities;

namespace Application.UseCases.Courses.Queries;

public class GetCourseByIdQuery(int id) : IRequest<BaseApiResponse<CourseDto?>>
{
    public int Id { get; set; } = id;
}
public class GetCourseByIdQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetCourseByIdQueryHandler> Logger) : IRequestHandler<GetCourseByIdQuery, BaseApiResponse<CourseDto?>>
{
    public async Task<BaseApiResponse<CourseDto?>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting {Course} with Id {Id}", nameof(Course), request.Id);
        var model = await UnitOfWork.Courses.GetById(request.Id)
        ?? throw new NotFoundException(nameof(Course), request.Id);
        return BaseApiResponse<CourseDto?>.Success(
            data: CourseDto.FromEntit(model),
            message: $"{nameof(Course)} Found Successfully"
        );
    }
}

