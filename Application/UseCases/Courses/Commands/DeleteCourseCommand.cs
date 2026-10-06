using Application.DTOs.Courses;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Courses.Commands;

public class DeleteCourseCommand : IRequest<bool>
{
    public required int Id { get; set; }
}
public class DeleteCourseCommandHandler(IUnitOfWork UnitOfWork, ILogger<DeleteCourseCommandHandler> Logger) : IRequestHandler<DeleteCourseCommand, bool>
{
    public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Deleting course with id {id}", request.Id);
        await UnitOfWork.Courses.Remove(request.Id);
        var state = await UnitOfWork.Complete();
        return state > 0;
    }
}