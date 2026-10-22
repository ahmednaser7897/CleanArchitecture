using Application.DTOs.Response;
using Application.DTOs.Sections;
using Domain.AppConstants;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Params;
using Domain.Specification.Sections;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sections.Queries;

public class GetAllSectionsQuery(PaginationParams paginationParams) : IRequest<BaseApiResponse<List<SectionDto>>>
{
    public PaginationParams PaginationParams { get; set; } = paginationParams;
}

public class GetAllSectionsQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetAllSectionsQueryHandler> Logger) : IRequestHandler<GetAllSectionsQuery, BaseApiResponse<List<SectionDto>>>
{
    public async Task<BaseApiResponse<List<SectionDto>>> Handle(GetAllSectionsQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting All {Section}s With Pagination", nameof(Section));
        var models = (await UnitOfWork.Sections.GetAllWithSpec(new GetSectionSpec(request.PaginationParams)))
                      .ConvertAll(c => SectionDto.FromEntit(c));

        var count = await UnitOfWork.Sections.CountWithSpec(
            new GetSectionSpec(new PaginationParams
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
        return BaseApiResponse<List<SectionDto>>.Success(data: models, pagination: pagination);
    }
}
