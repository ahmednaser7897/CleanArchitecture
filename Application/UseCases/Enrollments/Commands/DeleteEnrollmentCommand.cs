using Application.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Enrollments.Commands;

public class DeleteEnrollmentCommand : IRequest<BaseApiResponse<bool>>
{
    public required int Id { get; set; }
}

public class DeleteEnrollmentCommandHandler(IUnitOfWork UnitOfWork, ILogger<DeleteEnrollmentCommandHandler> Logger) : IRequestHandler<DeleteEnrollmentCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Deleting {Enrollment} With Id {Id}", nameof(Enrollment), request.Id);
        var model = await UnitOfWork.Enrollments.GetById(request.Id) ?? throw new NotFoundException(nameof(Enrollment), request.Id);
        await UnitOfWork.Enrollments.Remove(model.Id);
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Enrollment)} deleted successfully") : BaseApiResponse<bool>.Fail("Failed to delete enrollment");
    }
}
