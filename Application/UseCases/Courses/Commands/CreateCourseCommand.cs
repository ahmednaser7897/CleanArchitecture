using Application.DTOs.Courses;
using Application.DTOs.Response;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Courses.Commands;

public class CreateCourseCommand : IRequest<BaseApiResponse<bool>>
{
    public required CourseDto Dto { get; set; }
}
public class CreateCourseCommandHandler(IUnitOfWork UnitOfWork, ILogger<CreateCourseCommandHandler> Logger) : IRequestHandler<CreateCourseCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Creating {Course} With Name {Name}", nameof(Course), request.Dto.Name);
        await UnitOfWork.Courses.Add(request.Dto.ToEntit());
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Course)} created successfully") : BaseApiResponse<bool>.Fail("Failed to create course");
    }
}