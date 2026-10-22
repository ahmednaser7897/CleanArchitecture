using Application.DTOs.Response;
using Application.DTOs.Schedules;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Schedules.Commands;

public class CreateScheduleCommand : IRequest<BaseApiResponse<bool>>
{
    public required ScheduleDto Dto { get; set; }
}

public class CreateScheduleCommandHandler(IUnitOfWork UnitOfWork, ILogger<CreateScheduleCommandHandler> Logger) : IRequestHandler<CreateScheduleCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Creating {Schedule} With Title {Title}", nameof(Schedule), request.Dto.Title);
        await UnitOfWork.Schedules.Add(request.Dto.ToEntit());
        var state = await UnitOfWork.Complete();
        return state > 0 ? BaseApiResponse<bool>.Success(true, $"{nameof(Schedule)} created successfully") : BaseApiResponse<bool>.Fail("Failed to create schedule");
    }
}
