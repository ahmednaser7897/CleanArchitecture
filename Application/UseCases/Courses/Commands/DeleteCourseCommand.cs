using Application.DTOs.Response;
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
        Logger.LogInformation("Deleting course with id {id}", request.Id);
        await UnitOfWork.Courses.Remove(request.Id);
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, "Course deleted successfully") : throw new NotFoundException("Course", request.Id);
    }
}