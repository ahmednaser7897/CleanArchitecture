using Application.DTOs.Response;
using Application.DTOs.Schedules;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Schedules.Commands;

public class UpdateScheduleCommand : IRequest<BaseApiResponse<bool>>
{
    public required ScheduleDto Dto { get; set; }
    public required int Id { get; set; }
}

public class UpdateScheduleCommandHandler(IUnitOfWork UnitOfWork, ILogger<UpdateScheduleCommandHandler> Logger) : IRequestHandler<UpdateScheduleCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Updating {Schedule} with Id {Id}", nameof(Schedule), request.Id);
        var model = await UnitOfWork.Schedules.GetById(request.Id);
        if (model == null)
            throw new NotFoundException(nameof(Schedule), request.Id);
        request.Dto.Id = model.Id;
        await UnitOfWork.Schedules.Update(request.Dto.ToEntit());
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Schedule)} updated successfully") : BaseApiResponse<bool>.Fail("Failed to update schedule");
    }
}
