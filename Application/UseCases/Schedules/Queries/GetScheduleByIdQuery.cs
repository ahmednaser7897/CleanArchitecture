using Application.DTOs.Response;
using Application.DTOs.Schedules;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Schedules.Queries;

public class GetScheduleByIdQuery(int id) : IRequest<BaseApiResponse<ScheduleDto?>>
{
    public int Id { get; set; } = id;
}

public class GetScheduleByIdQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetScheduleByIdQueryHandler> Logger) : IRequestHandler<GetScheduleByIdQuery, BaseApiResponse<ScheduleDto?>>
{
    public async Task<BaseApiResponse<ScheduleDto?>> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting {Schedule} with Id {Id}", nameof(Schedule), request.Id);
        var model = await UnitOfWork.Schedules.GetById(request.Id)
        ?? throw new NotFoundException(nameof(Schedule), request.Id);
        return BaseApiResponse<ScheduleDto?>.Success(
            data: ScheduleDto.FromEntit(model),
            message: $"{nameof(Schedule)} Found Successfully"
        );
    }
}
