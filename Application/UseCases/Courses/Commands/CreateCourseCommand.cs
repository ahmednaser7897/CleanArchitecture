using Application.DTOs.Courses;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Courses.Commands;

public class CreateCourseCommand : IRequest<bool>
{
    public required CourseDto CourseDto { get; set; }
}
public class CreateCourseCommandHandler(IUnitOfWork UnitOfWork, ILogger<CreateCourseCommandHandler> Logger) : IRequestHandler<CreateCourseCommand, bool>
{
    public async Task<bool> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Creating course with name {name}", request.CourseDto.Name);
        await UnitOfWork.Courses.Add(request.CourseDto.ToEntit());
        var state = await UnitOfWork.Complete();
        return state > 0;
    }
}