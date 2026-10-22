using Application.DTOs.Instructors;
using Application.DTOs.Response;
using Domain.AppConstants;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Instructors;
using Domain.Specification.Params;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Instructors.Queries;

public class GetAllInstructorsQuery(PaginationParams paginationParams) : IRequest<BaseApiResponse<List<InstructorDto>>>
{
    public PaginationParams PaginationParams { get; set; } = paginationParams;
}

public class GetAllInstructorsQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetAllInstructorsQueryHandler> Logger) : IRequestHandler<GetAllInstructorsQuery, BaseApiResponse<List<InstructorDto>>>
{
    public async Task<BaseApiResponse<List<InstructorDto>>> Handle(GetAllInstructorsQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting All {Instructor}s With Pagination", nameof(Instructor));
        var models = (await UnitOfWork.Instructors.GetAllWithSpec(new GetInstructorSpec(request.PaginationParams)))
                      .ConvertAll(c => InstructorDto.FromEntit(c));

        var count = await UnitOfWork.Instructors.CountWithSpec(
            new GetInstructorSpec(new PaginationParams
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
        return BaseApiResponse<List<InstructorDto>>.Success(data: models, pagination: pagination);
    }
}
