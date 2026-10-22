using Application.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Schedules.Commands;

public class DeleteScheduleCommand : IRequest<BaseApiResponse<bool>>
{
    public required int Id { get; set; }
}

public class DeleteScheduleCommandHandler(IUnitOfWork UnitOfWork, ILogger<DeleteScheduleCommandHandler> Logger) : IRequestHandler<DeleteScheduleCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Deleting {Schedule} With Id {Id}", nameof(Schedule), request.Id);
        var model = await UnitOfWork.Schedules.GetById(request.Id);
        if (model == null)
            throw new NotFoundException(nameof(Schedule), request.Id);
        await UnitOfWork.Schedules.Remove(model.Id);
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Schedule)} deleted successfully") : BaseApiResponse<bool>.Fail("Failed to delete schedule");
    }
}
