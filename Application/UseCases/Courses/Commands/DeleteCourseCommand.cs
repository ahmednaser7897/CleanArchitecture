using Application.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Courses.Commands;

public class DeleteCourseCommand : IRequest<BaseApiResponse<bool>>
{
    public required int Id { get; set; }
}
public class DeleteCourseCommandHandler(IUnitOfWork UnitOfWork, ILogger<DeleteCourseCommandHandler> Logger) : IRequestHandler<DeleteCourseCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Deleting {Course} With Id {Id}", nameof(Course), request.Id);
        var model = await UnitOfWork.Courses.GetById(request.Id);
        if (model == null)
            throw new NotFoundException(nameof(Course), request.Id);
        await UnitOfWork.Courses.Remove(model.Id);
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Course)} deleted successfully") : BaseApiResponse<bool>.Fail("Failed to delete course");
    }
}