using Application.DTOs.Enrollments;
using Application.DTOs.Response;
using Domain.AppConstants;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Enrollments;
using Domain.Specification.Params;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Enrollments.Queries;

public class GetAllEnrollmentsQuery(PaginationParams paginationParams) : IRequest<BaseApiResponse<List<EnrollmentDto>>>
{
    public PaginationParams PaginationParams { get; set; } = paginationParams;
}

public class GetAllEnrollmentsQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetAllEnrollmentsQueryHandler> Logger) : IRequestHandler<GetAllEnrollmentsQuery, BaseApiResponse<List<EnrollmentDto>>>
{
    public async Task<BaseApiResponse<List<EnrollmentDto>>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting All {Enrollment}s With Pagination", nameof(Enrollment));
        var models = (await UnitOfWork.Enrollments.GetAllWithSpec(new GetEnrollmentSpec(request.PaginationParams)))
                      .ConvertAll(c => EnrollmentDto.FromEntit(c));

        var count = await UnitOfWork.Enrollments.CountWithSpec(
            new GetEnrollmentSpec(new PaginationParams
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
        return BaseApiResponse<List<EnrollmentDto>>.Success(data: models, pagination: pagination);
    }
}
