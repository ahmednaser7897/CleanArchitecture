using Application.DTOs.Courses;
using Application.DTOs.Response;
using Domain.AppConstants;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Courses;
using Domain.Specification.Params;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Courses.Queries;

public class GetAllCoursesQuery(PaginationParams paginationParams) : IRequest<BaseApiResponse<List<CourseDto>>>
{
    public PaginationParams PaginationParams { get; set; } = paginationParams;
}

public class GetAllCoursesQueryHandler(IUnitOfWork UnitOfWork, ILogger<GetAllCoursesQueryHandler> Logger) : IRequestHandler<GetAllCoursesQuery, BaseApiResponse<List<CourseDto>>>
{
    public async Task<BaseApiResponse<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting all courses");
        //Get all courses with pagination and filter
        var courses = (await UnitOfWork.Courses.GetAllWithSpec(new GetCourseSpec(request.PaginationParams)))
                      .ConvertAll(c => CourseDto.FromEntit(c));
        //Get count of all courses with same filter (without pagination)
        var count = await UnitOfWork.Courses.CountWithSpec(
            new GetCourseSpec(new PaginationParams
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
        return BaseApiResponse<List<CourseDto>>.Success(data: courses, pagination: pagination);
    }
}


