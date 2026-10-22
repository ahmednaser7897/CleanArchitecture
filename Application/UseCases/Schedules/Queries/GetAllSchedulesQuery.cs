using Application.DTOs.Response;
using Application.DTOs.Schedules;
using Domain.AppConstants;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Params;
using Domain.Specification.Schedules;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Schedules.Queries;

public class GetAllSchedulesQuery(PaginationParams paginationParams) : IRequest<BaseApiResponse<List<ScheduleDto>>>
{
    public PaginationParams PaginationParams { get; set; } = paginationParams;
}

public class GetAllSchedulesQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetAllSchedulesQueryHandler> Logger) : IRequestHandler<GetAllSchedulesQuery, BaseApiResponse<List<ScheduleDto>>>
{
    public async Task<BaseApiResponse<List<ScheduleDto>>> Handle(GetAllSchedulesQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting All {Schedule}s With Pagination", nameof(Schedule));
        var models = (await UnitOfWork.Schedules.GetAllWithSpec(new GetScheduleSpec(request.PaginationParams)))
                      .ConvertAll(c => ScheduleDto.FromEntit(c));

        var count = await UnitOfWork.Schedules.CountWithSpec(
            new GetScheduleSpec(new PaginationParams
            {
                Search = request.PaginationParams.Search,
                PageIndex = null,
                PageSize = null
            }));

        PagedResponse? pagination = null;
        if (request.PaginationParams.PageIndex != null)
        {
            pagination = new PagedResponse(
                pageIndex: request.PaginationParams.PageIndex.Value,
                pageSize: request.PaginationParams.PageSize ?? MyAppConstants.PaginationPageSize,
                totalCount: count);
        }
        return BaseApiResponse<List<ScheduleDto>>.Success(data: models, pagination: pagination);
    }
}
