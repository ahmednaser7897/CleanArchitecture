using Application.DTOs.Courses;
using Application.DTOs.Response;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Courses.Commands;

public class UpdateCourseCommand : IRequest<BaseApiResponse<bool>>
{
    public required CourseDto CourseDto { get; set; }
    public required int Id { get; set; }

}
public class UpdateCourseCommandHandler(IUnitOfWork UnitOfWork, ILogger<UpdateCourseCommandHandler> Logger) : IRequestHandler<UpdateCourseCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Updating course with id {id}", request.Id);
        var course = await UnitOfWork.Courses.GetById(request.Id);
        if (course == null)
            throw new NotFoundException("Course", request.Id);
        request.CourseDto.Id = request.Id;
        await UnitOfWork.Courses.Update(request.CourseDto.ToEntit());
        await UnitOfWork.Complete();
        return BaseApiResponse<bool>.Success(true, "Course updated successfully");
    }
}