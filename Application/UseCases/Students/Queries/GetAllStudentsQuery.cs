using Application.DTOs.Response;
using Application.DTOs.Students;
using Domain.AppConstants;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Params;
using Domain.Specification.Students;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Students.Queries;

public class GetAllStudentsQuery(PaginationParams paginationParams) : IRequest<BaseApiResponse<List<StudentDto>>>
{
    public PaginationParams PaginationParams { get; set; } = paginationParams;
}

public class GetAllStudentsQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetAllStudentsQueryHandler> Logger) : IRequestHandler<GetAllStudentsQuery, BaseApiResponse<List<StudentDto>>>
{
    public async Task<BaseApiResponse<List<StudentDto>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting All {Student}s With Pagination", nameof(Student));
        var models = (await UnitOfWork.Students.GetAllWithSpec(new GetStudentSpec(request.PaginationParams)))
                      .ConvertAll(c => StudentDto.FromEntit(c));

        var count = await UnitOfWork.Students.CountWithSpec(
            new GetStudentSpec(new PaginationParams
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
        return BaseApiResponse<List<StudentDto>>.Success(data: models, pagination: pagination);
    }
}
