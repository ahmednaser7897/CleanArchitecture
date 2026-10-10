using Application.DTOs.Courses;
using Application.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Courses.Commands;

public class UpdateCourseCommand : IRequest<BaseApiResponse<bool>>
{
    public required CourseDto Dto { get; set; }
    public required int Id { get; set; }

}
public class UpdateCourseCommandHandler(IUnitOfWork UnitOfWork, ILogger<UpdateCourseCommandHandler> Logger) : IRequestHandler<UpdateCourseCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Updating {Course} with Id {Id}", nameof(Course), request.Id);
        var model = await UnitOfWork.Courses.GetById(request.Id);
        if (model == null)
            throw new NotFoundException(nameof(Course), request.Id);
        request.Dto.Id = model.Id;
        await UnitOfWork.Courses.Update(request.Dto.ToEntit());
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Course)} updated successfully") : BaseApiResponse<bool>.Fail("Failed to update course");
    }
}