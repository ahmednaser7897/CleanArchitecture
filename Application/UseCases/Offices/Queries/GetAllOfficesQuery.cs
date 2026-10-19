using Application.DTOs.Offices;
using Application.DTOs.Response;
using Domain.AppConstants;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Courses;
using Domain.Specification.Params;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Offices.Queries;

public class GetAllOfficesQuery(PaginationParams paginationParams) : IRequest<BaseApiResponse<List<OfficeDto>>>
{
    public PaginationParams PaginationParams { get; set; } = paginationParams;
}

public class GetAllOfficesQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetAllOfficesQueryHandler> Logger) : IRequestHandler<GetAllOfficesQuery, BaseApiResponse<List<OfficeDto>>>
{
    public async Task<BaseApiResponse<List<OfficeDto>>> Handle(GetAllOfficesQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting All {Office}s With Pagination", nameof(Office));
        //Get all Data with pagination and filter
        var models = (await UnitOfWork.Offices.GetAllWithSpec(new GetOfficeSpec(request.PaginationParams)))
                      .ConvertAll(c => OfficeDto.FromEntit(c));
        //Get count of all Data with same filter (without pagination)
        var count = await UnitOfWork.Offices.CountWithSpec(
            new GetOfficeSpec(new PaginationParams
            {
                Search = request.PaginationParams.Search,
                PageIndex = null,
                PageSize = null
            }));
        //create pagination object if request.PaginationParams.PageIndex is not null
        // otherwise set pagination to null
        PagedResponse? pagination = null;
        if (request.PaginationParams.PageIndex != null)
        {
            pagination = new PagedResponse(
                pageIndex: request.PaginationParams.PageIndex.Value,
                pageSize: request.PaginationParams.PageSize ?? MyAppConstants.PaginationPageSize,
                totalCount: count);
        }
        return BaseApiResponse<List<OfficeDto>>.Success(data: models, pagination: pagination);
    }
}


