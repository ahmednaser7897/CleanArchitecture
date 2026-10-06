using Application.DTOs.Courses;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Courses.Commands;

public class UpdateCourseCommand : IRequest<bool>
{
    public required CourseDto CourseDto { get; set; }
    public required int Id { get; set; }

}
public class UpdateCourseCommandHandler(IUnitOfWork UnitOfWork, ILogger<UpdateCourseCommandHandler> Logger) : IRequestHandler<UpdateCourseCommand, bool>
{
    public async Task<bool> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Updating course with id {id}", request.Id);
        var course = await UnitOfWork.Courses.GetById(request.Id);
        if (course == null)
        {
            return false;
        }
        await UnitOfWork.Courses.Update(request.CourseDto.ToEntit());
        var state = await UnitOfWork.Complete();
        return state > 0;
    }
}